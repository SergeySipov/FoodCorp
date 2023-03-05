using FoodCorp.API.ViewModels.Account;
using FoodCorp.BusinessLogic.Models.Account;
using Mapster;

namespace FoodCorp.API.Mappers.AccountMapper;

[Mapper]
public interface IAccountMapper
{
    LoginModel MapTo(LoginViewModel loginViewModel);
    RegistrationModel MapTo(RegistrationViewModel loginViewModel);
}