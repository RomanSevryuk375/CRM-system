using Microsoft.EntityFrameworkCore;

namespace CRM.Shared.Infrastructure.OutboxMessages;

public sealed class OutboxRepository<TDbContext>(TDbContext dbContext)
    : IOutboxRepository<TDbContext>
    where TDbContext : DbContext
{
    public async Task<IReadOnlyList<OutboxMessage>> GetPendingMessagesAsync(
        int batchSize,
        CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<OutboxMessage>()
            .Where(m => m.ProcessedOnUtc == null)
            .OrderBy(m => m.OccurredOnUtc)
            .Take(batchSize)
            .ToListAsync(cancellationToken);
    }
}