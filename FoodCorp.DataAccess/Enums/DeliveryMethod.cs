using System.ComponentModel;

namespace FoodCorp.DataAccess.Enums;

public enum DeliveryMethod
{
    [Description("Classic Delivery")]
    ClassicDelivery = 1,

    [Description("Self Delivery")]
    SelfDelivery = 2,

    [Description("Door Delivery")]
    DoorDelivery = 3
}