namespace CRM.Shared.Infrastructure.OutboxMessages;

public interface IOutboxRepository
{
    Task<IReadOnlyList<OutboxMessage>> GetPendingMessagesAsync(
        int batchSize,
        CancellationToken cancellationToken = default);
}
