using System.Data;
using FoodCorp.DataAccess.Constants;
using FoodCorp.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodCorp.DataAccess.EntityConfiguration;

public class OrderOfferConfiguration : IEntityTypeConfiguration<OrderOffer>
{
    public void Configure(EntityTypeBuilder<OrderOffer> builder)
    {
        builder.ToTable(DatabaseTableNameConstants.OrderOffer, DatabaseSchemaNameConstants.Order);

        builder.HasKey(o => o.Id);

        builder.Property(o => o.CreatedDateTimeUtc)
            .IsRequired()
            .HasColumnType(SqlDbType.SmallDateTime.ToString())
            .HasDefaultValueSql(SqlServerFunctionConstants.GetUtcDate);

        builder.Property(o => o.Price)
            .HasColumnType(SqlDbType.Decimal.ToString())
            .HasPrecision(9) //specify precision and scale
            .IsRequired();

        builder.HasOne(o => o.Order)
            .WithMany(o => o.Offers)
            .HasForeignKey(o => o.OrderId);

        builder.HasOne(o => o.Performer)
            .WithMany(p => p.Offers)
            .HasForeignKey(o => o.PerformerId)
            .OnDelete(DeleteBehavior.NoAction); //multiple cascade path

        builder.Property(o => o.IsSelected)
            .IsRequired();
    }
}
