using System.Data;
using FoodCorp.DataAccess.Constants;
using FoodCorp.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodCorp.DataAccess.EntityConfiguration;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable(DatabaseTableNameConstants.Customer, DatabaseSchemaNameConstants.User);

        builder.HasKey(c => c.UserId);
        
        builder.HasOne(c => c.User)
            .WithOne(u => u.Customer)
            .HasForeignKey<Customer>(c => c.UserId);

        builder.Property(c => c.Rating)
            .IsRequired()
            .HasColumnType(SqlDbType.Float.ToString())
            .HasPrecision(2);
    }
}
