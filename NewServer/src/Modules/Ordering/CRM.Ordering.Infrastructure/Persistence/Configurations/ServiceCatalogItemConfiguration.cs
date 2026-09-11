using CRM.Ordering.Domain.Entities;
using CRM.Shared.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Ordering.Infrastructure.Persistence.Configurations;

internal sealed class ServiceCatalogItemConfiguration : IEntityTypeConfiguration<ServiceCatalogItem>
{
    public void Configure(EntityTypeBuilder<ServiceCatalogItem> builder)
    {
        builder.ToTable("service_catalog_items", OrderingDbContext.Schema);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();

        builder.Property(x => x.Title)
            .HasMaxLength(ServiceCatalogItem.MaxTitleLength)
            .IsRequired();

        builder.Property(x => x.Category)
            .HasMaxLength(ServiceCatalogItem.MaxCategoryLength)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(ServiceCatalogItem.MaxDescriptionLength)
            .IsRequired(false);

        builder.Property(x => x.StandardTime).IsRequired();

        builder.ConfigureBaseEntity();

        builder.HasIndex(x => x.Title);
        builder.HasIndex(x => x.Category);
    }
}
