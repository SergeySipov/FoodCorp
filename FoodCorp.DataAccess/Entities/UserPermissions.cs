namespace FoodCorp.DataAccess.Entities;

public class UserPermissions
{
    public int UserId { get; set; }
    public User User { get; set; }

    public int PermissionsBitMask { get; set; }
}
