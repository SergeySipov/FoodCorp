using FoodCorp.API.Mappers.AccountMapper;
using FoodCorp.API.ViewModels.Account;
using FoodCorp.BusinessLogic.Services.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodCorp.API.Controllers;

public class AccountController : ApiBaseController
{
    private readonly IAccountService _accountService;
    private readonly IAccountMapper _accountMapper;

    public AccountController(IAccountService accountService, 
        IAccountMapper accountMapper)
    {
        _accountService = accountService;
        _accountMapper = accountMapper;
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
        var jwtToken = await _accountService.RegisterAsync(registrationModel);

        return Ok(jwtToken);
    }
}
