using CRM.Inventory.Domain.Entities;
using CRM.Inventory.Infrastructure.Persistence;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Inventory.Infrastructure.Persistence.Configurations;

internal sealed class PartCategoryConfiguration : IEntityTypeConfiguration<PartCategory>
{
    public void Configure(EntityTypeBuilder<PartCategory> builder)
    {
        builder.ToTable("part_categories", InventoryDbContext.Schema);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Id,
                value => new PartCategoryId(value))
            .IsRequired();

        builder.Property(x => x.Name)
            .HasConversion(
                vo => vo.Value,
                dbVal => Name.Create(dbVal).Value)
            .HasMaxLength(Name.MaxLength)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(500)
            .IsRequired(false);

        builder.HasIndex(x => x.Name);
    }
}
