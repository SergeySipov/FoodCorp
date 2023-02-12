namespace FoodCorp.DataAccess.Entities;

public class PerformerProduct
{
    public int PerformerId { get; set; }
    public Performer Performer { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; }
}
