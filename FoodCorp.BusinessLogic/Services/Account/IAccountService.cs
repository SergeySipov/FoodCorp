using FoodCorp.BusinessLogic.Models.Account;

namespace FoodCorp.BusinessLogic.Services.Account;

public interface IAccountService
{
    Task<string> LoginUserAsync(LoginModel loginModel);
    Task<string> LoginOrRegisterWithGoogleAsync(GoogleLoginModel googleLoginModel);
    Task<string> LoginOrRegisterWithFacebookAsync(string credentials);
    Task<string> CompleteEmailConfirmationAsync(string email, string confirmToken);
    Task<string> RegisterAndGetJwtTokenAsync(RegistrationModel registrationModel);
    Task<string> RegisterAngGetEmailConfirmationTokenAsync(RegistrationModel registrationModel);
}