namespace FoodCorp.DataAccess.DataModels;

public class ProductDataModel
{
    public string Name { get; set; }
    public Enums.Category Category { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
}
