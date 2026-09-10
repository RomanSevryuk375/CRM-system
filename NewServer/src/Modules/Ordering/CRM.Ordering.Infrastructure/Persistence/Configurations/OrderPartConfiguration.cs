using CRM.Ordering.Domain.Entities.Orders;
using CRM.Ordering.Infrastructure.Persistence;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Ordering.Infrastructure.Persistence.Configurations;

internal sealed class OrderPartConfiguration : IEntityTypeConfiguration<OrderPart>
{
    public void Configure(EntityTypeBuilder<OrderPart> builder)
    {
        builder.ToTable("order_parts", OrderingDbContext.Schema);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Id,
                value => new OrderPartId(value))
            .IsRequired();

        builder.Property(x => x.OrderId)
            .HasConversion(
                id => id.Id,
                value => new OrderId(value))
            .IsRequired();

        builder.Property(x => x.PartId)
            .HasConversion(
                id => id.Id,
                value => new PartId(value))
            .IsRequired();

        builder.Property(x => x.IsProposed).IsRequired();

        builder.Property(x => x.Quantity)
            .HasPrecision(18, 3)
            .IsRequired();

        builder.Property(x => x.SoldPrice)
            .HasConversion(
                vo => vo.Value,
                dbVal => Money.Create(dbVal).Value)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.HasIndex(x => x.OrderId);
        builder.HasIndex(x => x.PartId);
    }
}
