using FoodCorp.DataAccess.Enums;

namespace FoodCorp.BusinessLogic.Models;

public class ProductModel
{
    public string Name { get; set; }
    public Category Category { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
}
