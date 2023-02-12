using FoodCorp.Common.Helpers;
using FoodCorp.DataAccess.Constants;
using FoodCorp.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodCorp.DataAccess.EntityConfiguration;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable(DatabaseTableNameConstants.Role, DatabaseSchemaNameConstants.Ref);

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(48);

        var roleClassModels = EnumHelper.ConvertToModel<Enums.Role>();
        var roleEntities = roleClassModels.Select(r => new Role
        {
            Id = (Enums.Role)r.Id,
            Name = r.Name
        }).ToList();

        builder.HasData(roleEntities);
    }
}