using FoodCorp.Common.Helpers;
using FoodCorp.DataAccess.Constants;
using FoodCorp.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodCorp.DataAccess.EntityConfiguration;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable(DatabaseTableNameConstants.Category, DatabaseSchemaNameConstants.Ref);

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(48);

        var categoryClassModels = EnumHelper.ConvertToModel<Enums.Category>();
        var categoryEntities = categoryClassModels.Select(r => new Category
        {
            Id = (Enums.Category)r.Id,
            Name = r.Name
        }).ToList();

        builder.HasData(categoryEntities);
    }
}