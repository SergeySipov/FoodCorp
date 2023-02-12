namespace FoodCorp.DataAccess.Entities;

public class Role
{
    public Enums.Role Id { get; set; }
    public string Name { get; set; }

    public ICollection<User> Users { get; set; }
}