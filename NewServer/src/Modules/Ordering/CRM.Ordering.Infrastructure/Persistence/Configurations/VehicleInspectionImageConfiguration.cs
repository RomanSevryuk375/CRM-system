using CRM.Ordering.Domain.Entities.VehicleInspections;
using CRM.Ordering.Infrastructure.Persistence;
using CRM.Shared.Abstractions.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Ordering.Infrastructure.Persistence.Configurations;

internal sealed class VehicleInspectionImageConfiguration : IEntityTypeConfiguration<VehicleInspectionImage>
{
    public void Configure(EntityTypeBuilder<VehicleInspectionImage> builder)
    {
        builder.ToTable("vehicle_inspection_images", OrderingDbContext.Schema);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Id,
                value => new VehicleInspectionImageId(value))
            .IsRequired();

        builder.Property(x => x.InspectionId)
            .HasConversion(
                id => id.Id,
                value => new VehicleInspectionId(value))
            .IsRequired();

        builder.Property(x => x.Path)
            .HasMaxLength(VehicleInspectionImage.MaxPathLength)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(VehicleInspectionImage.MaxDescriptionLength)
            .IsRequired(false);

        builder.HasIndex(x => x.InspectionId);
    }
}
