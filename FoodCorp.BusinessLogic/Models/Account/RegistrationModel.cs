namespace FoodCorp.BusinessLogic.Models.Account;

public record RegistrationModel(
    string Name,
    string Surname,
    string UserName,
    string Email,
    string PhoneNumber,
    string Password,
    string ProfileImagePath
);