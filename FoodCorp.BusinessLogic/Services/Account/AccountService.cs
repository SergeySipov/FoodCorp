using System.Security.Authentication;
using FoodCorp.BusinessLogic.Extensions;
using FoodCorp.BusinessLogic.Mappers.AccountMapper;
using FoodCorp.BusinessLogic.Models.Account;
using FoodCorp.BusinessLogic.Models.Account.Facebook;
using FoodCorp.BusinessLogic.Services.JwtToken;
using FoodCorp.Configuration.Model.AppSettings;
using FoodCorp.DataAccess.Entities;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Role = FoodCorp.DataAccess.Enums.Role;

namespace FoodCorp.BusinessLogic.Services.Account;

public class AccountService : IAccountService
{
    private const string TokenValidationUrl =
        "https://graph.facebook.com/debug_token?input_token={0}&access_token={1}|{2}";

    private const string UserInfoUrl =
        "https://graph.facebook.com/me?fields=first_name,last_name,email,id,picture&access_token={0}";

    private readonly UserManager<User> _userManager;
    private readonly IAccountMapper _accountMapper;
    private readonly IJwtTokenGenerationService _jwtTokenGenerationService;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IOptions<FacebookAuthenticationSettings> _facebookAuthSettings;

    public AccountService(UserManager<User> userManager,
        IAccountMapper accountMapper,
        IJwtTokenGenerationService jwtTokenGenerationService,
        IHttpClientFactory httpClientFactory, 
        IOptions<FacebookAuthenticationSettings> facebookAuthSettings)
    {
        _userManager = userManager;
        _accountMapper = accountMapper;
        _jwtTokenGenerationService = jwtTokenGenerationService;
        _httpClientFactory = httpClientFactory;
        _facebookAuthSettings = facebookAuthSettings;
    }

    public async Task<string> LoginUserAsync(LoginModel loginModel)
    {
        var user = await _userManager.FindByEmailAsync(loginModel.Email);

        if (user == null)
        {
            throw new AuthenticationException(loginModel.Email);
        }

        var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginModel.Password);

        if (!isPasswordValid)
        {
            throw new AuthenticationException(loginModel.Email);
        }

        var jwtToken = _jwtTokenGenerationService.GenerateJwt(user.Id, user.Email);
        return jwtToken;
    }

    public async Task<string> RegisterAsync(RegistrationModel registrationModel)
    {
        var user = _accountMapper.MapTo(registrationModel);

        user.Role = Role.NaturalPerson;
        user.RegistrationDateTimeUtc = DateTime.UtcNow;

        var identityResult = await _userManager.CreateAsync(user, registrationModel.Password);
        identityResult.ThrowExceptionOnFailure();

        var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        var jwtToken = _jwtTokenGenerationService.GenerateJwt(user.Id, user.Email);
        return jwtToken;
    }

    public async Task<string> LoginOrRegisterWithGoogleAsync(GoogleLoginModel googleLoginModel)
    {
        var jwtToken = string.Empty;

        var payload = await GoogleJsonWebSignature.ValidateAsync(googleLoginModel.IdToken, new GoogleJsonWebSignature.ValidationSettings());
        var user = await _userManager.FindByEmailAsync(payload.Email);
        if (user == null)
        {
            var newUser = new RegistrationModel(payload.GivenName,
                payload.FamilyName,
                payload.Name,
                payload.Email,
                string.Empty,
                string.Empty,
                payload.Picture);

            jwtToken = await RegisterAsync(newUser);

            user = await _userManager.FindByEmailAsync(payload.Email);
        }

        var identityResult = await _userManager.AddLoginAsync(user, new UserLoginInfo(
            googleLoginModel.IdentityProviderId,
            googleLoginModel.ProviderKey,
            "Google"));

        identityResult.ThrowExceptionOnFailure();

        if (string.IsNullOrEmpty(jwtToken))
        {
            jwtToken = _jwtTokenGenerationService.GenerateJwt(user.Id, user.Email);
        }
        return jwtToken;
    }

    public async Task<string> LoginOrRegisterWithFacebookAsync(string credentials)
    {
        var httpClient = _httpClientFactory.CreateClient("FacebookHttpClientConnection");

        var formattedTokenValidationUrl = string.Format(TokenValidationUrl, credentials,
            _facebookAuthSettings.Value.AppId, _facebookAuthSettings.Value.AppSecret);
        var debugTokenResponse = await httpClient.GetAsync(formattedTokenValidationUrl);

        var stringThing = await debugTokenResponse.Content.ReadAsStringAsync();
        var userModel = JsonConvert.DeserializeObject<FacebookUserModel>(stringThing);

        if (userModel == null || !userModel.UserInfo.IsValid)
        {
            throw new AuthenticationException();
        }

        var formattedUserInfoUrl = string.Format(UserInfoUrl, credentials);
        var meResponse = await httpClient.GetAsync(formattedUserInfoUrl);
        var userContent = await meResponse.Content.ReadAsStringAsync();
        var facebookUserInfo = JsonConvert.DeserializeObject<FacebookUserInfo>(userContent);

        if (facebookUserInfo == null)
        {
            throw new AuthenticationException();
        }

        var jwtToken = string.Empty;
        var user = await _userManager.FindByEmailAsync(facebookUserInfo.Email);
        if (user == null)
        {
            var newUser = new RegistrationModel(facebookUserInfo.FirstName,
                facebookUserInfo.LastName,
                string.Empty,
                facebookUserInfo.Email,
                string.Empty,
                string.Empty,
                facebookUserInfo.Image.ImageUrl);

            jwtToken = await RegisterAsync(newUser);

            user = await _userManager.FindByEmailAsync(facebookUserInfo.Email);
        }

        //var identityResult = await _userManager.AddLoginAsync(user, new UserLoginInfo(
        //    googleLoginModel.IdentityProviderId,
        //    googleLoginModel.ProviderKey,
        //    "Facebook"));

        //identityResult.ThrowExceptionOnFailure();

        if (string.IsNullOrEmpty(jwtToken))
        {
            jwtToken = _jwtTokenGenerationService.GenerateJwt(user.Id, user.Email);
        }
        return jwtToken;
    }
}