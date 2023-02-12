﻿using FoodCorp.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodCorp.DataAccess.DatabaseContext;

public class FoodCorpDbContext : DbContext
{
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserShowcaseImage> UserImages { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Performer> Performers { get; set; }
    public DbSet<CustomerProduct> CustomerProducts { get; set; }
    public DbSet<PerformerProduct> PerformerProducts { get; set; }
    public DbSet<OrderStatus> OrderStatuses { get; set; }
    public DbSet<Order> Orders { get; set; }

    public FoodCorpDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(Console.WriteLine);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FoodCorpDbContext).Assembly);
    }
}