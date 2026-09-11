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
        builder.Property(x => x.Id).IsRequired();
        builder.Property(x => x.OrderId).IsRequired();
        builder.Property(x => x.PartId).IsRequired();
        builder.Property(x => x.IsProposed).IsRequired();

        builder.Property(x => x.Quantity)
            .HasPrecision(OrderPart.QuantityPrecision, OrderPart.QuantityScale)
            .IsRequired();

        builder.Property(x => x.SoldPrice).IsRequired();

        builder.HasIndex(x => x.OrderId);
        builder.HasIndex(x => x.PartId);
    }
}
