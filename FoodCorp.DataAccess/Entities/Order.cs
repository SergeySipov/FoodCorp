namespace FoodCorp.DataAccess.Entities;

public class Order
{
    public int Id { get; set; }
    public DateTime CreatedDateTimeUtc { get; set; }
    public DateTime? ExpirationDateTimeUtc { get; set; }
    public decimal PreferredPrice { get; set; }
    public int Count { get; set; }
    public Enums.OrderStatus Status { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; }

    public int CustomerId { get; set; }
    public Customer Customer { get; set; }

}
