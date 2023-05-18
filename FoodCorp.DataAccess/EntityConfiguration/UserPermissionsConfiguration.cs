using FoodCorp.DataAccess.Constants;
using FoodCorp.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodCorp.DataAccess.EntityConfiguration;

public class UserPermissionsConfiguration : IEntityTypeConfiguration<UserPermissions>
{
    public void Configure(EntityTypeBuilder<UserPermissions> builder)
    {
        builder.ToTable(DatabaseTableNameConstants.UserPermissions, DatabaseSchemaNameConstants.User);

        builder.HasKey(up => up.UserId);

        builder.HasOne(up => up.User)
            .WithOne(u => u.Permissions)
            .HasForeignKey<UserPermissions>(up => up.UserId);
    }
}
