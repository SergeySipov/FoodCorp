using FoodCorp.BusinessLogic.Mappers.AccountMapper;
using FoodCorp.BusinessLogic.Models.Account;
using FoodCorp.DataAccess.Entities;

namespace FoodCorp.BusinessLogic.Mappers.AccountMapper
{
    public partial class AccountMapper : IAccountMapper
    {
        public User MapTo(RegistrationModel p1)
        {
            return p1 == null ? null : new User()
            {
                Name = p1.Name,
                Surname = p1.Surname,
                UserName = p1.UserName,
                Email = p1.Email,
                PhoneNumber = p1.PhoneNumber
            };
        }
    }
}