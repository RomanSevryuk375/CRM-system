using CRM.Inventory.Domain.Entities;
using CRM.Inventory.Infrastructure.Persistence;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD.ValueObjects;
using CRM.Shared.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Inventory.Infrastructure.Persistence.Configurations;

internal sealed class PositionConfiguration : IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> builder)
    {
        builder.ToTable("positions", InventoryDbContext.Schema);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();
        builder.Property(x => x.PartId).IsRequired();
        builder.Property(x => x.CellId).IsRequired();
        builder.Property(x => x.PurchasePrice).IsRequired();
        builder.Property(x => x.SellingPrice).IsRequired();

        builder.Property(x => x.Quantity)
            .HasPrecision(Position.QuantityPrecision, Position.QuantityScale)
            .IsRequired();

        builder.ConfigureBaseEntity();

        builder.HasOne<StorageCell>()
            .WithMany()
            .HasForeignKey(x => x.CellId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Part>()
            .WithMany()
            .HasForeignKey(x => x.PartId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.PartId);
        builder.HasIndex(x => x.CellId);
    }
}
