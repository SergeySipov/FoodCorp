using FoodCorp.API.Mappers.AccountMapper;
using FoodCorp.API.ViewModels.Account;
using FoodCorp.BusinessLogic.Models.Account;

namespace FoodCorp.API.Mappers.AccountMapper
{
    public partial class AccountMapper : IAccountMapper
    {
        public LoginModel MapTo(LoginViewModel p1)
        {
            return p1 == null ? null : new LoginModel()
            {
                Email = p1.Email,
                Password = p1.Password,
                IsPersistent = p1.IsPersistent
            };
        }
        public RegistrationModel MapTo(RegistrationViewModel p2)
        {
            return p2 == null ? null : new RegistrationModel()
            {
                Name = p2.Name,
                Surname = p2.Surname,
                UserName = p2.UserName,
                Email = p2.Email,
                PhoneNumber = p2.PhoneNumber,
                Password = p2.Password
            };
        }
    }
}