using System.Security.Authentication;
using FoodCorp.BusinessLogic.Extensions;
using FoodCorp.BusinessLogic.Mappers.AccountMapper;
using FoodCorp.BusinessLogic.Models.Account;
using FoodCorp.BusinessLogic.Services.JwtToken;
using FoodCorp.Configuration.Model.AppSettings;
using FoodCorp.DataAccess.Entities;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Role = FoodCorp.DataAccess.Enums.Role;

namespace FoodCorp.BusinessLogic.Services.Account;

public class AccountService : IAccountService
{
    private readonly UserManager<User> _userManager;
    private readonly IAccountMapper _accountMapper;
    private readonly IJwtTokenGenerationService _jwtTokenGenerationService;
    private readonly IOptions<GoogleAuthenticationSettings> _googleAuthSettings;

    public AccountService(UserManager<User> userManager,
        IAccountMapper accountMapper,
        IJwtTokenGenerationService jwtTokenGenerationService,
        IOptions<GoogleAuthenticationSettings> googleAuthSettings)
    {
        _userManager = userManager;
        _accountMapper = accountMapper;
        _jwtTokenGenerationService = jwtTokenGenerationService;
        _googleAuthSettings = googleAuthSettings;
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
        }

        user = await _userManager.FindByEmailAsync(payload.Email);
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
}