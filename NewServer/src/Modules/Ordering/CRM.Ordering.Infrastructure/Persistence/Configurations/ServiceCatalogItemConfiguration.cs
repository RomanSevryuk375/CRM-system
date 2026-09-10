using CRM.Ordering.Domain.Entities;
using CRM.Ordering.Infrastructure.Persistence;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Ordering.Infrastructure.Persistence.Configurations;

internal sealed class ServiceCatalogItemConfiguration : IEntityTypeConfiguration<ServiceCatalogItem>
{
    public void Configure(EntityTypeBuilder<ServiceCatalogItem> builder)
    {
        builder.ToTable("service_catalog_items", OrderingDbContext.Schema);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Id,
                value => new JobId(value))
            .IsRequired();

        builder.Property(x => x.Title)
            .HasMaxLength(ServiceCatalogItem.MaxTitleLength)
            .IsRequired();

        builder.Property(x => x.Category)
            .HasMaxLength(ServiceCatalogItem.MaxCategoryLength)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(ServiceCatalogItem.MaxDescriptionLength)
            .IsRequired(false);

        builder.Property(x => x.StandardTime)
            .HasConversion(
                vo => vo.Value,
                dbVal => StandardHours.Create(dbVal).Value)
            .HasPrecision(StandardHours.Precision, StandardHours.Scale)
            .IsRequired();

        builder.Property(x => x.IsDeleted).IsRequired();
        builder.Property(x => x.DeletedAt).IsRequired(false);
        builder.Property(x => x.DeletedBy).IsRequired(false);

        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.CreatedBy).IsRequired();
        builder.Property(x => x.UpdatedAt).IsRequired(false);
        builder.Property(x => x.UpdatedBy).IsRequired(false);

        builder.Property(x => x.Version).IsConcurrencyToken();
        builder.HasQueryFilter(x => !x.IsDeleted);

        builder.HasIndex(x => x.Title);
        builder.HasIndex(x => x.Category);
    }
}
