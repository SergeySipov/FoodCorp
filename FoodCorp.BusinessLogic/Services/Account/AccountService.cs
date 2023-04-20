using System.Security.Authentication;
using FoodCorp.BusinessLogic.Constants;
using FoodCorp.BusinessLogic.Extensions;
using FoodCorp.BusinessLogic.Mappers.AccountMapper;
using FoodCorp.BusinessLogic.Models.Account;
using FoodCorp.BusinessLogic.Models.Account.Facebook;
using FoodCorp.BusinessLogic.Services.JwtToken;
using FoodCorp.Configuration.Model.AppSettings;
using FoodCorp.DataAccess.Entities;
using FoodCorp.DataAccess.Repositories.UserRepository;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Role = FoodCorp.DataAccess.Enums.Role;

namespace FoodCorp.BusinessLogic.Services.Account;

public class AccountService : IAccountService
{
    private readonly UserManager<User> _userManager;
    private readonly IAccountMapper _accountMapper;
    private readonly IJwtTokenGenerationService _jwtTokenGenerationService;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IOptions<FacebookAuthenticationSettings> _facebookAuthSettings;
    private readonly IUserRepository _userRepository;

    public AccountService(UserManager<User> userManager,
        IAccountMapper accountMapper,
        IJwtTokenGenerationService jwtTokenGenerationService,
        IHttpClientFactory httpClientFactory,
        IOptions<FacebookAuthenticationSettings> facebookAuthSettings, 
        IUserRepository userRepository)
    {
        _userManager = userManager;
        _accountMapper = accountMapper;
        _jwtTokenGenerationService = jwtTokenGenerationService;
        _httpClientFactory = httpClientFactory;
        _facebookAuthSettings = facebookAuthSettings;
        _userRepository = userRepository;
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

        var userPermissionsBitMask = await _userRepository.GetUserPermissionsBitMaskAsync(user.Id);
        var jwtToken = _jwtTokenGenerationService.GenerateJwt(user.Id, user.Email, userPermissionsBitMask);
        return jwtToken;
    }

    public async Task<string> RegisterAngGetEmailConfirmationTokenAsync(RegistrationModel registrationModel)
    {
        var user = await RegisterAsync(registrationModel);

        var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        return emailConfirmationToken;
    }

    public async Task<string> RegisterAndGetJwtTokenAsync(RegistrationModel registrationModel)
    {
        var user = await RegisterAsync(registrationModel);
        var userPermissionsBitMask = await _userRepository.GetUserPermissionsBitMaskAsync(user.Id);

        var jwtToken = _jwtTokenGenerationService.GenerateJwt(user.Id, user.Email, userPermissionsBitMask);

        return jwtToken;
    }

    public async Task<string> LoginOrRegisterWithGoogleAsync(GoogleLoginModel googleLoginModel)
    {
        var payload = await GoogleJsonWebSignature.ValidateAsync(googleLoginModel.IdToken,
            new GoogleJsonWebSignature.ValidationSettings());

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

            user = await RegisterAsync(newUser, true);
            return _jwtTokenGenerationService.GenerateJwt(user.Id, user.Email, 0);
        }

        var userPermissionsBitMask = await _userRepository.GetUserPermissionsBitMaskAsync(user.Id);
        return _jwtTokenGenerationService.GenerateJwt(user.Id, user.Email, userPermissionsBitMask);
    }

    public async Task<string> LoginOrRegisterWithFacebookAsync(string credentials)
    {
        var httpClient = _httpClientFactory.CreateClient(HttpClientConnectionNameConstants.Facebook);

        var formattedTokenValidationUrl = string.Format(AuthenticationUrlConstants.TokenValidationUrl, credentials,
            _facebookAuthSettings.Value.AppId, _facebookAuthSettings.Value.AppSecret);
        var debugTokenResponse = await httpClient.GetAsync(formattedTokenValidationUrl);

        var stringThing = await debugTokenResponse.Content.ReadAsStringAsync();
        var userModel = JsonConvert.DeserializeObject<FacebookUserModel>(stringThing);

        if (userModel == null || !userModel.UserInfo.IsValid)
        {
            throw new AuthenticationException();
        }

        var formattedUserInfoUrl = string.Format(AuthenticationUrlConstants.UserInfoUrl, credentials);
        var meResponse = await httpClient.GetAsync(formattedUserInfoUrl);
        var userContent = await meResponse.Content.ReadAsStringAsync();
        var facebookUserInfo = JsonConvert.DeserializeObject<FacebookUserInfo>(userContent);

        if (facebookUserInfo == null)
        {
            throw new AuthenticationException();
        }

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

            user = await RegisterAsync(newUser, true);
            return _jwtTokenGenerationService.GenerateJwt(user.Id, user.Email, 0);
        }

        var userPermissionsBitMask = await _userRepository.GetUserPermissionsBitMaskAsync(user.Id);
        return _jwtTokenGenerationService.GenerateJwt(user.Id, user.Email, userPermissionsBitMask);
    }

    public async Task<string> CompleteEmailConfirmationAsync(string email, string confirmToken)
    {
        var user = await _userManager.FindByEmailAsync(email);

        var confirmEmailIdentityResult = await _userManager.ConfirmEmailAsync(user, confirmToken);
        confirmEmailIdentityResult.ThrowExceptionOnFailure();

        var userPermissionsBitMask = await _userRepository.GetUserPermissionsBitMaskAsync(user.Id);
        return _jwtTokenGenerationService.GenerateJwt(user.Id, user.Email, userPermissionsBitMask);
    }

    private async Task<User> RegisterAsync(RegistrationModel registrationModel, bool isEmailConfirmed = false)
    {
        var user = _accountMapper.MapTo(registrationModel);

        user.Role = Role.NaturalPerson;
        user.RegistrationDateTimeUtc = DateTime.UtcNow;
        user.EmailConfirmed = isEmailConfirmed;

        var identityResult = await _userManager.CreateAsync(user, registrationModel.Password);
        identityResult.ThrowExceptionOnFailure();

        return user;
    }
}