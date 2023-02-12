using FoodCorp.DataAccess.Constants;
using FoodCorp.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodCorp.DataAccess.EntityConfiguration;

public class UserShowcaseImageConfiguration : IEntityTypeConfiguration<UserShowcaseImage>
{
    public void Configure(EntityTypeBuilder<UserShowcaseImage> builder)
    {
        builder.ToTable(DatabaseTableNameConstants.UserShowcaseImage, DatabaseSchemaNameConstants.Dbo);

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Path)
            .IsRequired()
            .HasMaxLength(1024);

        builder.HasOne(u => u.User)
            .WithMany(u => u.ShowcaseImages)
            .HasForeignKey(u => u.UserId);
    }
}