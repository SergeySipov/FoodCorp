using System.Data;
using FoodCorp.DataAccess.Constants;
using FoodCorp.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodCorp.DataAccess.EntityConfiguration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    private const string CategoryIdColumnName = "CategoryId";

    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable(DatabaseTableNameConstants.Product, DatabaseSchemaNameConstants.Product);

        builder.HasKey(u => u.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(p => p.Description)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(p => p.Price)
            .HasColumnType(SqlDbType.Decimal.ToString())
            .HasPrecision(9) //specify precision and scale
            .IsRequired();

        builder.HasMany(u => u.Images)
            .WithOne(s => s.Product)
            .HasForeignKey(s => s.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Category>()
            .WithMany(r => r.Products)
            .HasForeignKey(u => u.Category)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Property(u => u.Category)
            .HasColumnName(CategoryIdColumnName);
    }
}
