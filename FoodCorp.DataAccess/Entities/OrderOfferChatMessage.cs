namespace FoodCorp.DataAccess.Entities;

public class OrderOfferChatMessage
{
    public int Id { get; set; }
    public DateTime CreatedDateTimeUtc { get; set; }
    public string Message { get; set; }

    public int OrderOfferId { get; set; }
    public OrderOffer OrderOffer { get; set; }
}
