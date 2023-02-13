using System.Data;
using FoodCorp.DataAccess.Constants;
using FoodCorp.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodCorp.DataAccess.EntityConfiguration;

public class OrderOfferChatMessageConfiguration : IEntityTypeConfiguration<OrderOfferChatMessage>
{
    public void Configure(EntityTypeBuilder<OrderOfferChatMessage> builder)
    {
        builder.ToTable(DatabaseTableNameConstants.OrderOfferChatMessage, DatabaseSchemaNameConstants.Order);

        builder.HasKey(o => o.Id);

        builder.Property(o => o.CreatedDateTimeUtc)
            .IsRequired()
            .HasColumnType(SqlDbType.SmallDateTime.ToString())
            .HasDefaultValueSql(SqlServerFunctionConstants.GetUtcDate);

        builder.Property(o => o.Message)
            .IsRequired()
            .HasMaxLength(2048);

        builder.HasOne(o => o.OrderOffer)
            .WithMany(o => o.ChatMessages)
            .HasForeignKey(o => o.OrderOfferId);
    }
}
