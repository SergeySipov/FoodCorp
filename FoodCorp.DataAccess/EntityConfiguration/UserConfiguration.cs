using System.Data;
using FoodCorp.DataAccess.Constants;
using FoodCorp.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodCorp.DataAccess.EntityConfiguration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    private const string RoleIdColumnName = "RoleId";

    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(DatabaseTableNameConstants.User, DatabaseSchemaNameConstants.User);

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(u => u.Name)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(u => u.Surname)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(u => u.NickName)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(u => u.ProfileImagePath)
            .HasMaxLength(1024);
            
        builder.Property(u => u.RegistrationDateTimeUtc)
            .IsRequired()
            .HasColumnType(SqlDbType.SmallDateTime.ToString())
            .HasDefaultValueSql(SqlServerFunctionConstants.GetUtcDate);

        builder.HasMany(u => u.ShowcaseImages)
            .WithOne(s => s.User)
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Role>()
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.Role)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Property(u => u.Role)
            .HasColumnName(RoleIdColumnName);
    }
}