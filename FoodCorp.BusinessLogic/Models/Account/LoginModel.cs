namespace FoodCorp.BusinessLogic.Models.Account;

public record LoginModel(
    string Email,
    string Password,
    bool IsPersistent
);