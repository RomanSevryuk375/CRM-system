using CRM.Inventory.Domain.Entities;
using CRM.Inventory.Infrastructure.Persistence;
using CRM.Shared.Abstractions.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Inventory.Infrastructure.Persistence.Configurations;

internal sealed class PartConfiguration : IEntityTypeConfiguration<Part>
{
    public void Configure(EntityTypeBuilder<Part> builder)
    {
        builder.ToTable("parts", InventoryDbContext.Schema);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Id,
                value => new PartId(value))
            .IsRequired();

        builder.Property(x => x.CategoryId)
            .HasConversion(
                id => id.Id,
                value => new PartCategoryId(value))
            .IsRequired();

        builder.HasOne<PartCategory>()
            .WithMany()
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.OemArticle)
            .HasMaxLength(Part.MaxArticleLength)
            .IsRequired(false);

        builder.Property(x => x.ManufacturerArticle)
            .HasMaxLength(Part.MaxArticleLength)
            .IsRequired(false);

        builder.Property(x => x.InternalArticle)
            .HasMaxLength(Part.MaxArticleLength)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(Part.MaxNameLength)
            .IsRequired();

        builder.Property(x => x.Manufacturer)
            .HasMaxLength(Part.MaxManufacturerLength)
            .IsRequired();

        builder.Property(x => x.Applicability)
            .HasMaxLength(Part.MaxApplicabilityLength)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(Part.MaxDescriptionLength)
            .IsRequired(false);

        builder.Property(x => x.IsDeleted).IsRequired();
        builder.Property(x => x.DeletedAt).IsRequired(false);
        builder.Property(x => x.DeletedBy).IsRequired(false);

        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.CreatedBy).IsRequired();
        builder.Property(x => x.UpdatedAt).IsRequired(false);
        builder.Property(x => x.UpdatedBy).IsRequired(false);

        builder.Property(x => x.Version).IsConcurrencyToken();
        builder.HasQueryFilter(x => !x.IsDeleted);

        builder.HasIndex(x => x.CategoryId);
        builder.HasIndex(x => x.InternalArticle).IsUnique();
        builder.HasIndex(x => x.OemArticle);
        builder.HasIndex(x => x.ManufacturerArticle);
        builder.HasIndex(x => x.Name);
    }
}
