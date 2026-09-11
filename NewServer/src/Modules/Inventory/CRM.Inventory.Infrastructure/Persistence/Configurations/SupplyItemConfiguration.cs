using CRM.Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Inventory.Infrastructure.Persistence.Configurations;

internal sealed class SupplyItemConfiguration : IEntityTypeConfiguration<SupplyItem>
{
    public void Configure(EntityTypeBuilder<SupplyItem> builder)
    {
        builder.ToTable("supply_items", InventoryDbContext.Schema);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();
        builder.Property(x => x.SupplyId).IsRequired();
        builder.Property(x => x.PositionId).IsRequired();

        builder.Property(x => x.Quantity)
            .HasPrecision(SupplyItem.QuantityPrecision, SupplyItem.QuantityScale)
            .IsRequired();

        builder.Property(x => x.Price).IsRequired();

        builder.HasOne<Position>()
            .WithMany()
            .HasForeignKey(x => x.PositionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.SupplyId);
        builder.HasIndex(x => x.PositionId);
    }
}
