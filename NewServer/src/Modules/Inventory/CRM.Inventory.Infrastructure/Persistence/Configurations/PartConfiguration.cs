using CRM.Inventory.Domain.Entities;
using CRM.Shared.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Inventory.Infrastructure.Persistence.Configurations;

internal sealed class PartConfiguration : IEntityTypeConfiguration<Part>
{
    public void Configure(EntityTypeBuilder<Part> builder)
    {
        builder.ToTable("parts", InventoryDbContext.Schema);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();
        builder.Property(x => x.CategoryId).IsRequired();

        builder.HasOne<PartCategory>()
            .WithMany()
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.InternalArticle)
            .HasMaxLength(Part.MaxArticleLength)
            .IsRequired();

        builder.Property(x => x.OemArticle)
            .HasMaxLength(Part.MaxArticleLength)
            .IsRequired(false);

        builder.Property(x => x.ManufacturerArticle)
            .HasMaxLength(Part.MaxArticleLength)
            .IsRequired(false);

        builder.Property(x => x.Name)
            .HasMaxLength(Part.MaxNameLength)
            .IsRequired();

        builder.Property(x => x.Manufacturer)
            .HasMaxLength(Part.MaxManufacturerLength)
            .IsRequired(false);

        builder.Property(x => x.Applicability)
            .HasMaxLength(Part.MaxApplicabilityLength)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(Part.MaxDescriptionLength)
            .IsRequired(false);

        builder.ConfigureBaseEntity();

        builder.HasIndex(x => x.CategoryId);
        builder.HasIndex(x => x.InternalArticle).IsUnique();
        builder.HasIndex(x => x.OemArticle);
        builder.HasIndex(x => x.ManufacturerArticle);
        builder.HasIndex(x => x.Name);
    }
}
