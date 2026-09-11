using CRM.Ordering.Domain.Entities.Orders;
using CRM.Ordering.Infrastructure.Persistence;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD.ValueObjects;
using CRM.Shared.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Ordering.Infrastructure.Persistence.Configurations;

internal sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("orders", OrderingDbContext.Schema);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();

        builder.Property(x => x.Status)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.CarId).IsRequired();
        builder.Property(x => x.WorkerId).IsRequired();
        builder.Property(x => x.StartedAt).IsRequired();
        builder.Property(x => x.PlannedFinishDate).IsRequired(false);

        builder.Property(x => x.Priority)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.Amount).IsRequired();

        builder.ConfigureBaseEntity();

        builder.HasIndex(x => x.Status);
        builder.HasIndex(x => x.CarId);
        builder.HasIndex(x => x.WorkerId);
        builder.HasIndex(x => x.StartedAt);

        builder.HasMany(x => x.Works)
            .WithOne()
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(x => x.Works)
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.HasMany(x => x.Parts)
            .WithOne()
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(x => x.Parts)
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.HasMany(x => x.Guarantees)
            .WithOne()
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(x => x.Guarantees)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
