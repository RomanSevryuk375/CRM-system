using CRM.Inventory.Infrastructure.Persistence;
using CRM.Shared.Infrastructure.Data.OutboxMessages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Inventory.Infrastructure.Persistence.Configurations;

internal sealed class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable("outbox_messages", InventoryDbContext.Schema);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Type).IsRequired();
        builder.Property(x => x.Content)
            .HasColumnType("jsonb")
            .IsRequired();
        builder.Property(x => x.OccurredOnUtc).IsRequired();
        builder.Property(x => x.ProcessedOnUtc).IsRequired(false);
        builder.Property(x => x.Error).IsRequired(false);

        builder.HasIndex(x => x.OccurredOnUtc);
    }
}
