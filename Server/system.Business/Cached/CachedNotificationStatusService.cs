using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using CRMSystem.Business.Extensions;

namespace CRMSystem.Business.Cached;

public class CachedNotificationStatusService(
    INotificationStatusService decorated,
    IDistributedCache distributed,
    ILogger<CachedNotificationStatusService> logger) : INotificationStatusService
{
    private const string CACHE_KEY = $"Dict_{nameof(CachedNotificationStatusService)}";

    public async Task<List<NotificationStatusItem>> GetNotificationStatuses(CancellationToken ct)
    {
        return await distributed.GetOrCreateAsync(
            CACHE_KEY,
            () => decorated.GetNotificationStatuses(ct),
            TimeSpan.FromHours(24),
            logger, ct) ?? [];
    }
}
