using CRM.Shared.Infrastructure.Data.InboxMessages;
using CRM.Shared.Infrastructure.Data.OutboxMessages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Shared.Infrastructure.Data.Extensions;

public static class MessageConfigurationExtensions
{
    public static EntityTypeBuilder<OutboxMessage> ConfigureOutboxMessage(
        this EntityTypeBuilder<OutboxMessage> builder,
        string schema)
    {
        builder.ToTable("outbox_messages", schema);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Type).IsRequired();
        builder.Property(x => x.Content)
            .HasColumnType("jsonb")
            .IsRequired();
        builder.Property(x => x.OccurredOnUtc).IsRequired();
        builder.Property(x => x.ProcessedOnUtc).IsRequired(false);
        builder.Property(x => x.Error).IsRequired(false);

        builder.HasIndex(x => x.OccurredOnUtc);

        return builder;
    }

    public static EntityTypeBuilder<InboxMessage> ConfigureInboxMessage(
        this EntityTypeBuilder<InboxMessage> builder,
        string schema)
    {
        builder.ToTable("inbox_messages", schema);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.ProcessedOnUtc).IsRequired();

        return builder;
    }
}
