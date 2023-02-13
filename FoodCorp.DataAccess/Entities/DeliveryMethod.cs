namespace FoodCorp.DataAccess.Entities;

public class DeliveryMethod
{
    public Enums.DeliveryMethod Id { get; set; }
    public string Name { get; set; }

    public ICollection<OrderDeliveryAndPaymentType> DeliveryAndPaymentTypes { get; set; }
}
