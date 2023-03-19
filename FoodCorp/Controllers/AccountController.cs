using FoodCorp.API.Mappers.AccountMapper;
using FoodCorp.API.ViewModels.Account;
using FoodCorp.BusinessLogic.Services.Account;
using FoodCorp.BusinessLogic.Services.Email;
using FoodCorp.Configuration.Model.AppSettings;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace FoodCorp.API.Controllers;

public class AccountController : ApiBaseController
{
    private readonly IAccountService _accountService;
    private readonly IAccountMapper _accountMapper;
    private readonly IOptions<SecuritySettings> _securityOptions;
    private readonly IEmailSenderService _emailSenderService;

    public AccountController(IAccountService accountService,
        IAccountMapper accountMapper, 
        IOptions<SecuritySettings> securityOptions, 
        IEmailSenderService emailSenderService)
    {
        _accountService = accountService;
        _accountMapper = accountMapper;
        _securityOptions = securityOptions;
        _emailSenderService = emailSenderService;
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
        
        if (_securityOptions.Value.RequireConfirmedEmail)
        {
            var emailConfirmationToken = _accountService.RegisterAngGetEmailConfirmationTokenAsync(registrationModel);

            var confirmationLink = Url.Action(nameof(ConfirmEmail), "Account",
                new { registrationModel.Email, confirmToken = emailConfirmationToken }, Request.Scheme);

            await _emailSenderService.SendEmailAsync(registrationModel.Email, "Confirm your email",
                $"Чтобы завершить регистрацию - перейдите по <a href='{confirmationLink}'>ссылке</a>");

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

    [Authorize]
    [HttpGet]
    public IActionResult LogOut()
    {
        //await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return new SignOutResult(new[]
        {
            CookieAuthenticationDefaults.AuthenticationScheme,
            //JwtBearerDefaults.AuthenticationScheme,
            //GoogleDefaults.AuthenticationScheme,
        });
    }
}