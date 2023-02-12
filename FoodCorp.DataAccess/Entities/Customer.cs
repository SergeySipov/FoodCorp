namespace FoodCorp.DataAccess.Entities;

public class Customer
{
    public int UserId { get; set; }
    public User User { get; set; }

    public double Rating { get; set; }

    public ICollection<CustomerProduct> Products { get; set; }
    public ICollection<Order> Orders { get; set; }
}
