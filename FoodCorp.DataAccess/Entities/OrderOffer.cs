namespace FoodCorp.DataAccess.Entities;

public class OrderOffer
{
    public int Id { get; set; }
    public DateTime CreatedDateTimeUtc { get; set; }
    public decimal Price { get; set; }
    public bool IsSelected { get; set; }

    public int OrderId { get; set; }
    public Order Order { get; set; }

    public int PerformerId { get; set; }
    public Performer Performer { get; set; }

    public ICollection<OrderOfferChatMessage> ChatMessages { get; set; }
    public OrderDeliveryAndPaymentType DeliveryAndPaymentType { get; set; }
}
