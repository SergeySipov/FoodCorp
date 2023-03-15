using FoodCorp.BusinessLogic.Models.Account;

namespace FoodCorp.BusinessLogic.Services.Account;

public interface IAccountService
{
    Task<string> LoginUserAsync(LoginModel loginModel);
    Task<string> RegisterAsync(RegistrationModel registrationModel);
    Task<string> LoginOrRegisterWithGoogleAsync(GoogleLoginModel googleLoginModel);
}