using FoodCorp.Common.Helpers;
using FoodCorp.DataAccess.Constants;
using FoodCorp.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodCorp.DataAccess.EntityConfiguration;

public class OrderStatusConfiguration : IEntityTypeConfiguration<OrderStatus>
{
    public void Configure(EntityTypeBuilder<OrderStatus> builder)
    {
        builder.ToTable(DatabaseTableNameConstants.OrderStatus, DatabaseSchemaNameConstants.Ref);

        builder.HasKey(os => os.Id);

        builder.Property(os => os.Name)
            .IsRequired()
            .HasMaxLength(48);

        var orderStatusClassModels = EnumHelper.ConvertToModel<Enums.OrderStatus>();
        var orderStatusEntities = orderStatusClassModels.Select(r => new OrderStatus
        {
            Id = (Enums.OrderStatus)r.Id,
            Name = r.Name
        }).ToList();

        builder.HasData(orderStatusEntities);
    }
}
