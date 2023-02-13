namespace FoodCorp.DataAccess.Entities;

public class OrderDeliveryAndPaymentType
{
    public int OrderOfferId { get; set; }
    public OrderOffer OrderOffer { get; set; }

    public Enums.DeliveryMethod DeliveryMethod { get; set; }
    public Enums.PaymentMethod PaymentMethod { get; set; }
}
