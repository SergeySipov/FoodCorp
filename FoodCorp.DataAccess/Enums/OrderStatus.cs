using System.ComponentModel;

namespace FoodCorp.DataAccess.Enums;

public enum OrderStatus
{
    [Description("Performer Found")]
    PerformerFound = 1,

    [Description("Performer Selected")]
    PerformerSelected = 2,

    [Description("In Progress")]
    InProgress = 3,

    [Description("Ready For Delivery")]
    ReadyForDelivery = 4,

    Completed = 5,
    Canceled = 6
}