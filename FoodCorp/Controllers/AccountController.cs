using FoodCorp.API.Constants.Resources;
using FoodCorp.API.Mappers.AccountMapper;
using FoodCorp.API.ViewModels.Account;
using FoodCorp.BusinessLogic.Services.Account;
using FoodCorp.BusinessLogic.Services.Email;
using FoodCorp.Configuration.Model.AppSettings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace FoodCorp.API.Controllers;

public class AccountController : ApiBaseController
{
    private readonly IAccountService _accountService;
    private readonly IAccountMapper _accountMapper;
    private readonly IOptions<IdentitySecuritySettings> _identitySecurityOptions;
    private readonly IEmailSenderService _emailSenderService;
    private readonly IStringLocalizer<AccountController> _stringLocalizer;

    public AccountController(IAccountService accountService,
        IAccountMapper accountMapper,
        IOptions<IdentitySecuritySettings> identitySecurityOptions,
        IEmailSenderService emailSenderService,
        IStringLocalizer<AccountController> stringLocalizer)
    {
        _accountService = accountService;
        _accountMapper = accountMapper;
        _identitySecurityOptions = identitySecurityOptions;
        _emailSenderService = emailSenderService;
        _stringLocalizer = stringLocalizer;
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> LoginOrRegisterWithFacebook([FromBody] string credential)
    {
        var jwtToken = await _accountService.LoginOrRegisterWithFacebookAsync(credential);

        return Ok(jwtToken);
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> LoginOrRegisterWithGoogle(GoogleLoginViewModel loginViewModel)
    {
        var googleLoginModel = _accountMapper.MapTo(loginViewModel);
        var jwtToken = await _accountService.LoginOrRegisterWithGoogleAsync(googleLoginModel);

        return Ok(jwtToken);
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginViewModel)
    {
        var loginModel = _accountMapper.MapTo(loginViewModel);
        var jwtToken = await _accountService.LoginUserAsync(loginModel);

        return Ok(jwtToken);
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Register(RegistrationViewModel registrationViewModel)
    {
        var registrationModel = _accountMapper.MapTo(registrationViewModel);

        if (_identitySecurityOptions.Value.RequireConfirmedEmail)
        {
            var emailConfirmationToken = await _accountService.RegisterAngGetEmailConfirmationTokenAsync(registrationModel);

            var confirmationLink = Url.Action(nameof(ConfirmEmail), "Account",
                new { registrationModel.Email, confirmToken = emailConfirmationToken }, Request.Scheme);
            
            var messageSubject = _stringLocalizer[ControllerResourcesKeyConstants.EmailConfirmationSubject];
            var messageBody = _stringLocalizer[ControllerResourcesKeyConstants.EmailConfirmationMessage];
            
            await _emailSenderService.SendEmailAsync(registrationModel.Email, 
                messageSubject,
                string.Format(messageBody, confirmationLink));

            return Ok();
        }

        var jwtToken = await _accountService.RegisterAndGetJwtTokenAsync(registrationModel);
        return Ok(jwtToken);
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> ConfirmEmail(string email, string confirmToken)
    {
        var jwtToken = await _accountService.CompleteEmailConfirmationAsync(email, confirmToken);
        return Ok(jwtToken);
    }
}