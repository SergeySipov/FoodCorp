using FoodCorp.DataAccess.Constants;
using FoodCorp.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Data;

namespace FoodCorp.DataAccess.EntityConfiguration;

public class PerformerConfiguration : IEntityTypeConfiguration<Performer>
{
    public void Configure(EntityTypeBuilder<Performer> builder)
    {
        builder.ToTable(DatabaseTableNameConstants.Performer, DatabaseSchemaNameConstants.User);

        builder.HasKey(p => p.UserId);

        builder.Property(p => p.CountOfCompletedOrders)
            .IsRequired();

        builder.HasOne(p => p.User)
            .WithOne(u => u.Performer)
            .HasForeignKey<Performer>(p => p.UserId);

        builder.Property(p => p.Rating)
            .IsRequired()
            .HasColumnType(SqlDbType.Float.ToString())
            .HasPrecision(2);

        builder.HasMany(c => c.Products)
            .WithMany(p => p.Performers)
            .UsingEntity<PerformerProduct>();
    }
}
