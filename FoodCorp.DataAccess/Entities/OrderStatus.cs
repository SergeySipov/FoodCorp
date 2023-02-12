namespace FoodCorp.DataAccess.Entities;

public class OrderStatus
{
    public Enums.OrderStatus Id { get; set; }
    public string Name { get; set; }

    public ICollection<Order> Orders { get; set; }
}
