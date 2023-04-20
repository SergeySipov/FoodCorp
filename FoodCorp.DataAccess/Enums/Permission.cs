namespace FoodCorp.DataAccess.Enums;

[Flags]
public enum Permission
{
    CanCreateOrder       = 1 << 0,
    CanPlaceOrderOffer   = 1 << 1,
    CanCreateProduct     = 1 << 2,
    CanModerateProducts  = 1 << 3,
    CanModerateFeedback  = 1 << 4,
    CanManageRoles       = 1 << 5,
    CanManagePermissions = 1 << 6,
}