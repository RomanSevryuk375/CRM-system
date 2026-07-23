using Microsoft.EntityFrameworkCore;

namespace CRM.Shared.Infrastructure.OutboxMessages;

public interface IOutboxRepository<TDbContext> where TDbContext : DbContext
{
    Task<IReadOnlyList<OutboxMessage>> GetPendingMessagesAsync(
        int batchSize,
        CancellationToken cancellationToken = default);
}