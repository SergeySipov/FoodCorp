using FoodCorp.API.Mappers.AccountMapper;
using FoodCorp.API.ViewModels.Account;
using FoodCorp.BusinessLogic.Models.Account;

namespace FoodCorp.API.Mappers.AccountMapper
{
    public partial class AccountMapper : IAccountMapper
    {
        public LoginModel MapTo(LoginViewModel p1)
        {
            return p1 == null ? null : new LoginModel(p1.Email, p1.Password, p1.IsPersistent);
        }
        public RegistrationModel MapTo(RegistrationViewModel p2)
        {
            return p2 == null ? null : new RegistrationModel(p2.Name, p2.Surname, p2.UserName, p2.Email, p2.PhoneNumber, p2.Password, p2.ProfileImagePath);
        }
        public GoogleLoginModel MapTo(GoogleLoginViewModel p3)
        {
            return p3 == null ? null : new GoogleLoginModel()
            {
                IdToken = p3.IdToken,
                IdentityProviderId = p3.IdentityProviderId,
                ProviderKey = p3.ProviderKey
            };
        }
    }
}