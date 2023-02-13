namespace FoodCorp.DataAccess.Entities;

public class OrderFeedback
{
    public int OrderId { get; set; }
    public Order Order { get; set; }

    public double Rating { get; set; }
    public string Comment { get; set; }
}
