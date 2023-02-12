using FoodCorp.DataAccess.Constants;
using FoodCorp.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodCorp.DataAccess.EntityConfiguration;

public class CustomerProductConfiguration : IEntityTypeConfiguration<CustomerProduct>
{
    public void Configure(EntityTypeBuilder<CustomerProduct> builder)
    {
        builder.ToTable(DatabaseTableNameConstants.CustomerProduct, DatabaseSchemaNameConstants.Product);

        builder.HasKey(cp => new { cp.CustomerId, cp.ProductId });

        builder.HasOne(cp => cp.Customer)
            .WithMany(c => c.Products)
            .HasForeignKey(cp => cp.CustomerId);
    }
}
