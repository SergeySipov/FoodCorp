using FoodCorp.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodCorp.DataAccess.DatabaseContext;

public class FoodCorpDbContext : DbContext
{
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserShowcaseImage> UserImages { get; set; }

    public FoodCorpDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.LogTo(Console.WriteLine);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FoodCorpDbContext).Assembly);
    }
}