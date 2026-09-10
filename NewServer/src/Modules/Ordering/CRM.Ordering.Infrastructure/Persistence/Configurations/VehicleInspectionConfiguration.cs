using CRM.Ordering.Domain.Entities.VehicleInspections;
using CRM.Ordering.Infrastructure.Persistence;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Ordering.Infrastructure.Persistence.Configurations;

internal sealed class VehicleInspectionConfiguration : IEntityTypeConfiguration<VehicleInspection>
{
    public void Configure(EntityTypeBuilder<VehicleInspection> builder)
    {
        builder.ToTable("vehicle_inspections", OrderingDbContext.Schema);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Id,
                value => new VehicleInspectionId(value))
            .IsRequired();

        builder.Property(x => x.OrderId)
            .HasConversion(
                id => id.Id,
                value => new OrderId(value))
            .IsRequired();

        builder.Property(x => x.WorkerId)
            .HasConversion(
                id => id.Id,
                value => new WorkerId(value))
            .IsRequired();

        builder.Property(x => x.Mileage)
            .HasConversion(
                vo => vo.Value,
                dbVal => Mileage.Create(dbVal).Value)
            .IsRequired();

        builder.Property(x => x.FuelLevel)
            .HasConversion(
                vo => vo.Value,
                dbVal => FuelLevel.Create(dbVal).Value)
            .IsRequired();

        builder.Property(x => x.CleanlinessLevel)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.PersonalBelongings)
            .HasMaxLength(2000)
            .IsRequired(false);

        builder.Property(x => x.DashboardWarnings)
            .HasMaxLength(2000)
            .IsRequired(false);

        builder.Property(x => x.ExternalDefects)
            .HasMaxLength(2000)
            .IsRequired(false);

        builder.Property(x => x.InternalDefects)
            .HasMaxLength(2000)
            .IsRequired(false);

        builder.Property(x => x.HasWheelNutKey).IsRequired();
        builder.Property(x => x.HasServiceBook).IsRequired();
        builder.Property(x => x.ClientSign).IsRequired();
        builder.Property(x => x.WorkerSign).IsRequired();

        builder.Property(x => x.Status)
            .HasConversion<int>()
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

        builder.HasIndex(x => x.OrderId);
        builder.HasIndex(x => x.WorkerId);
        builder.HasIndex(x => x.Status);

        builder.HasMany(x => x.Images)
            .WithOne()
            .HasForeignKey(x => x.InspectionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Metadata
            .FindNavigation(nameof(VehicleInspection.Images))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
