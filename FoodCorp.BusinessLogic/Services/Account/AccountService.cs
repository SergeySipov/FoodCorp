using System.Security.Authentication;
using FoodCorp.BusinessLogic.Extensions;
using FoodCorp.BusinessLogic.Mappers.AccountMapper;
using FoodCorp.BusinessLogic.Models.Account;
using FoodCorp.BusinessLogic.Services.JwtToken;
using FoodCorp.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Role = FoodCorp.DataAccess.Enums.Role;

namespace FoodCorp.BusinessLogic.Services.Account;

public class AccountService : IAccountService
{
    private readonly UserManager<User> _userManager;
    private readonly IAccountMapper _accountMapper;
    private readonly IJwtTokenGenerationService _jwtTokenGenerationService;

    public AccountService(UserManager<User> userManager,
        IAccountMapper accountMapper, 
        IJwtTokenGenerationService jwtTokenGenerationService)
    {
        _userManager = userManager;
        _accountMapper = accountMapper;
        _jwtTokenGenerationService = jwtTokenGenerationService;
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
}
