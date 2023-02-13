namespace FoodCorp.DataAccess.Entities;

public class PaymentMethod
{
    public Enums.PaymentMethod Id { get; set; }
    public string Name { get; set; }

    public ICollection<OrderDeliveryAndPaymentType> DeliveryAndPaymentTypes { get; set; }
}
