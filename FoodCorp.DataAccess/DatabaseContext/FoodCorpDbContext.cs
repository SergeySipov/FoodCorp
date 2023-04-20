using FoodCorp.DataAccess.Entities;
using FoodCorp.DataAccess.Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FoodCorp.DataAccess.DatabaseContext;

public class FoodCorpDbContext : IdentityUserContext<User, int>
{
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserShowcaseImage> UserImages { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Performer> Performers { get; set; }
    public DbSet<CustomerProduct> CustomerProducts { get; set; }
    public DbSet<PerformerProduct> PerformerProducts { get; set; }
    public DbSet<OrderStatus> OrderStatuses { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderFeedback> OrderFeedbacks { get; set; }
    public DbSet<DeliveryMethod> DeliveryMethods { get; set; }
    public DbSet<PaymentMethod> PaymentMethods { get; set; }
    public DbSet<OrderOffer> OrderOffers { get; set; }
    public DbSet<OrderOfferChatMessage> OrderOfferChatMessages { get; set; }
    public DbSet<OrderDeliveryAndPaymentType> OrderDeliveryAndPaymentTypes { get; set; }
    public DbSet<UserPermissions> UserPermissions { get; set; }

    public FoodCorpDbContext(DbContextOptions options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FoodCorpDbContext).Assembly);
        modelBuilder.ConfigureIdentityModels();
    }
}