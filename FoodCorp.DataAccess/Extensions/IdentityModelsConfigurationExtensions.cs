using FoodCorp.DataAccess.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FoodCorp.DataAccess.Extensions;

public static class IdentityModelsConfigurationExtensions
{
    public static void ConfigureIdentityModels(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IdentityUserClaim<int>>(cfg =>
        {
            cfg.ToTable(DatabaseTableNameConstants.UserClaim, DatabaseSchemaNameConstants.User);

            cfg.Property(u => u.ClaimType)
                .IsRequired()
                .HasMaxLength(256);

            cfg.Property(u => u.ClaimValue)
                .IsRequired()
                .HasMaxLength(256);
        });

        modelBuilder.Entity<IdentityUserLogin<int>>(cfg =>
        {
            cfg.ToTable(DatabaseTableNameConstants.UserLogin, DatabaseSchemaNameConstants.User);

            cfg.Property(u => u.ProviderDisplayName)
                .IsRequired()
                .HasMaxLength(450);
        });

        modelBuilder.Entity<IdentityUserToken<int>>(cfg =>
        {
            cfg.ToTable(DatabaseTableNameConstants.UserToken, DatabaseSchemaNameConstants.User);

            cfg.Property(u => u.Value)
                .IsRequired()
                .HasMaxLength(450);
        });
    }
}
