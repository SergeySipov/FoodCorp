using FoodCorp.DataAccess.Constants;
using FoodCorp.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodCorp.DataAccess.EntityConfiguration;

public class PerformerProductConfiguration : IEntityTypeConfiguration<PerformerProduct>
{
    public void Configure(EntityTypeBuilder<PerformerProduct> builder)
    {
        builder.ToTable(DatabaseTableNameConstants.PerformerProduct, DatabaseSchemaNameConstants.Product);

        builder.HasKey(pp => new { pp.PerformerId, pp.ProductId });

        builder.HasOne(pp => pp.Performer)
            .WithMany(p => p.Products)
            .HasForeignKey(cp => cp.PerformerId);
    }
}
