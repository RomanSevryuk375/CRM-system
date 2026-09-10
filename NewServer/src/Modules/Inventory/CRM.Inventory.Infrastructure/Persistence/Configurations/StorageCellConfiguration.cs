using CRM.Inventory.Domain.Entities;
using CRM.Inventory.Infrastructure.Persistence;
using CRM.Shared.Abstractions.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Inventory.Infrastructure.Persistence.Configurations;

internal sealed class StorageCellConfiguration : IEntityTypeConfiguration<StorageCell>
{
    public void Configure(EntityTypeBuilder<StorageCell> builder)
    {
        builder.ToTable("storage_cells", InventoryDbContext.Schema);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Id,
                value => new StorageCellId(value))
            .IsRequired();

        builder.Property(x => x.Rack)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Shelf)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasIndex(x => new { x.Rack, x.Shelf }).IsUnique();
    }
}
