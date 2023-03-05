using FoodCorp.BusinessLogic.Models.Account;
using FoodCorp.DataAccess.Entities;
using Mapster;

namespace FoodCorp.BusinessLogic.Mappers.AccountMapper;

[Mapper]
public interface IAccountMapper
{
    User MapTo(RegistrationModel registrationModel);
}