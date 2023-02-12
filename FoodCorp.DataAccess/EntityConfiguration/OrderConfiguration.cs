using System.Data;
using FoodCorp.DataAccess.Constants;
using FoodCorp.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodCorp.DataAccess.EntityConfiguration;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public const string OrderStatusIdColumnName = "OrderStatusId";

    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable(DatabaseTableNameConstants.Order, DatabaseSchemaNameConstants.Order);

        builder.HasKey(o => o.Id);

        builder.Property(o => o.CreatedDateTimeUtc)
            .IsRequired()
            .HasColumnType(SqlDbType.SmallDateTime.ToString())
            .HasDefaultValueSql(SqlServerFunctionConstants.GetUtcDate);

        builder.Property(o => o.ExpirationDateTimeUtc)
            .HasColumnType(SqlDbType.SmallDateTime.ToString());

        builder.Property(o => o.PreferredPrice)
            .HasColumnType(SqlDbType.Decimal.ToString())
            .HasPrecision(9) //specify precision and scale
            .IsRequired();

        builder.Property(o => o.Count)
            .IsRequired();

        builder.HasOne<OrderStatus>()
            .WithMany(os => os.Orders)
            .HasForeignKey(u => u.Status)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Property(o => o.Status)
            .HasColumnName(OrderStatusIdColumnName);

        builder.HasOne(o => o.Product)
            .WithMany(p => p.Orders)
            .HasForeignKey(o => o.ProductId);

        builder.HasOne(o => o.Customer)
            .WithMany(c => c.Orders)
            .HasForeignKey(o => o.CustomerId);
    }
}
