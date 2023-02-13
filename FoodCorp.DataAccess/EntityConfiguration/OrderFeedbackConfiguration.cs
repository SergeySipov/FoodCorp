using System.Data;
using FoodCorp.DataAccess.Constants;
using FoodCorp.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodCorp.DataAccess.EntityConfiguration;

public class OrderFeedbackConfiguration : IEntityTypeConfiguration<OrderFeedback>
{
    public void Configure(EntityTypeBuilder<OrderFeedback> builder)
    {
        builder.ToTable(DatabaseTableNameConstants.OrderFeedback, DatabaseSchemaNameConstants.Order);

        builder.HasKey(of => of.OrderId);

        builder.Property(of => of.Rating)
            .IsRequired()
            .HasColumnType(SqlDbType.Float.ToString())
            .HasPrecision(2);

        builder.HasOne(of => of.Order)
            .WithOne(o => o.OrderFeedback)
            .HasForeignKey<OrderFeedback>(of => of.OrderId);

        builder.Property(of => of.Comment)
            .IsRequired()
            .HasMaxLength(2048);
    }
}
