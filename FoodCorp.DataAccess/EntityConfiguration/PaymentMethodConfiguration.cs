using FoodCorp.Common.Helpers;
using FoodCorp.DataAccess.Constants;
using FoodCorp.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodCorp.DataAccess.EntityConfiguration;

public class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
{
    public void Configure(EntityTypeBuilder<PaymentMethod> builder)
    {
        builder.ToTable(DatabaseTableNameConstants.PaymentMethod, DatabaseSchemaNameConstants.Ref);

        builder.HasKey(pm => pm.Id);

        builder.Property(pm => pm.Name)
            .IsRequired()
            .HasMaxLength(48);

        var paymentMethodClassModels = EnumHelper.ConvertToModel<Enums.PaymentMethod>();
        var paymentMethodEntities = paymentMethodClassModels.Select(r => new PaymentMethod
        {
            Id = (Enums.PaymentMethod)r.Id,
            Name = r.Name
        }).ToList();

        builder.HasData(paymentMethodEntities);
    }
}
