using FoodCorp.DataAccess.Constants;
using FoodCorp.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodCorp.DataAccess.EntityConfiguration;

public class OrderDeliveryAndPaymentTypeConfiguration : IEntityTypeConfiguration<OrderDeliveryAndPaymentType>
{
    private const string DeliveryMethodIdColumnName = "DeliveryMethodId";
    private const string PaymentMethodIdColumnName = "PaymentMethodId";

    public void Configure(EntityTypeBuilder<OrderDeliveryAndPaymentType> builder)
    {
        builder.ToTable(DatabaseTableNameConstants.OrderDeliveryAndPaymentType, DatabaseSchemaNameConstants.Order);

        builder.HasKey(o => o.OrderOfferId);

        builder.HasOne(o => o.OrderOffer)
            .WithOne(o => o.DeliveryAndPaymentType)
            .HasForeignKey<OrderDeliveryAndPaymentType>(c => c.OrderOfferId);

        builder.HasOne<DeliveryMethod>()
            .WithMany(d => d.DeliveryAndPaymentTypes)
            .HasForeignKey(o => o.DeliveryMethod)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Property(o => o.DeliveryMethod)
            .HasColumnName(DeliveryMethodIdColumnName);

        builder.HasOne<PaymentMethod>()
            .WithMany(p => p.DeliveryAndPaymentTypes)
            .HasForeignKey(o => o.PaymentMethod)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Property(o => o.PaymentMethod)
            .HasColumnName(PaymentMethodIdColumnName);
    }
}
