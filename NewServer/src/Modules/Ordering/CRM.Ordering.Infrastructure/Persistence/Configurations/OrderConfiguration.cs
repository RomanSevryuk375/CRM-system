using CRM.Ordering.Domain.Entities.Orders;
using CRM.Ordering.Infrastructure.Persistence;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Ordering.Infrastructure.Persistence.Configurations;

internal sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("orders", OrderingDbContext.Schema);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Id,
                value => new OrderId(value))
            .IsRequired();

        builder.Property(x => x.Status)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.CarId)
            .HasConversion(
                id => id.Id,
                value => new CarId(value))
            .IsRequired();

        builder.Property(x => x.WorkerId)
            .HasConversion(
                id => id.Id,
                value => new WorkerId(value))
            .IsRequired();

        builder.Property(x => x.StartedAt).IsRequired();
        builder.Property(x => x.PlannedFinishDate).IsRequired(false);

        builder.Property(x => x.Priority)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.Amount)
            .HasConversion(
                vo => vo.Value,
                dbVal => Money.Create(dbVal).Value)
            .HasPrecision(Money.Precision, Money.Scale)
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

        builder.HasIndex(x => x.Status);
        builder.HasIndex(x => x.CarId);
        builder.HasIndex(x => x.WorkerId);
        builder.HasIndex(x => x.StartedAt);

        builder.HasMany(x => x.Works)
            .WithOne()
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Metadata
            .FindNavigation(nameof(Order.Works))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.HasMany(x => x.Parts)
            .WithOne()
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Metadata
            .FindNavigation(nameof(Order.Parts))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.HasMany(x => x.Guarantees)
            .WithOne()
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Metadata
            .FindNavigation(nameof(Order.Guarantees))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
