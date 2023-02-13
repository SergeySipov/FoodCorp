using FoodCorp.Common.Helpers;
using FoodCorp.DataAccess.Constants;
using FoodCorp.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodCorp.DataAccess.EntityConfiguration;

public class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
{
    public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
    {
        builder.ToTable(DatabaseTableNameConstants.DeliveryMethod, DatabaseSchemaNameConstants.Ref);

        builder.HasKey(dm => dm.Id);

        builder.Property(dm => dm.Name)
            .IsRequired()
            .HasMaxLength(48);

        var deliveryMethodClassModels = EnumHelper.ConvertToModel<Enums.DeliveryMethod>();
        var deliveryMethodEntities = deliveryMethodClassModels.Select(r => new DeliveryMethod
        {
            Id = (Enums.DeliveryMethod)r.Id,
            Name = r.Name
        }).ToList();

        builder.HasData(deliveryMethodEntities);
    }
}
