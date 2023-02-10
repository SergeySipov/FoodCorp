namespace FoodCorp.DataAccess.Entities
{
    public class UserShowcaseImage
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public string Path { get; set; }
    }
}
