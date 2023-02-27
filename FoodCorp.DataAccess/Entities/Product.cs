namespace FoodCorp.DataAccess.Entities;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Enums.Category Category { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }

    public ICollection<ProductImage> Images { get; set; }
    public ICollection<Customer> Customers { get; set; }
    public ICollection<Performer> Performers { get; set; }
    public ICollection<Order> Orders { get; set; }
}