using CRM.Inventory.Domain.Entities;
using CRM.Inventory.Infrastructure.Persistence;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Inventory.Infrastructure.Persistence.Configurations;

internal sealed class SupplyItemConfiguration : IEntityTypeConfiguration<SupplyItem>
{
    public void Configure(EntityTypeBuilder<SupplyItem> builder)
    {
        builder.ToTable("supply_items", InventoryDbContext.Schema);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Id,
                value => new SupplyItemId(value))
            .IsRequired();

        builder.Property(x => x.SupplyId)
            .HasConversion(
                id => id.Id,
                value => new SupplyId(value))
            .IsRequired();

        builder.Property(x => x.PositionId)
            .HasConversion(
                id => id.Id,
                value => new PositionId(value))
            .IsRequired();

        builder.HasOne<Position>()
            .WithMany()
            .HasForeignKey(x => x.PositionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.Quantity)
            .HasPrecision(SupplyItem.QuantityPrecision, SupplyItem.QuantityScale)
            .IsRequired();

        builder.Property(x => x.Price)
            .HasConversion(
                vo => vo.Value,
                dbVal => Money.Create(dbVal).Value)
            .HasPrecision(Money.Precision, Money.Scale)
            .IsRequired();

        builder.HasIndex(x => x.SupplyId);
        builder.HasIndex(x => x.PositionId);
    }
}
