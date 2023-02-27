namespace FoodCorp.DataAccess.Entities;

public class Performer
{
    public int UserId { get; set; }
    public User User { get; set; }

    public double Rating { get; set; }
    public int CountOfCompletedOrders { get; set; }

    public ICollection<Product> Products { get; set; }
    public ICollection<OrderOffer> Offers { get; set; }
}
