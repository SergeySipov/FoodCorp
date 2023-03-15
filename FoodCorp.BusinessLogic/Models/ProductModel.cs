using FoodCorp.DataAccess.Enums;

namespace FoodCorp.BusinessLogic.Models;

public record ProductModel(
    string Name,
    Category Category,
    decimal Price,
    string Description
);