using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using CRMSystem.Business.Extensions;

namespace CRMSystem.Business.Cached;

public class CachedNotificationTypeService(
    INotificationTypeService decorated,
    IDistributedCache distributed,
    ILogger<CachedNotificationTypeService> logger) : INotificationTypeService
{
    private const string CACHE_KEY = $"Dict_{nameof(CachedNotificationTypeService)}";

    public async Task<List<NotificationTypeItem>> GetNotificationTypes(CancellationToken ct)
    {
        return await distributed.GetOrCreateAsync(
            CACHE_KEY,
            () => decorated.GetNotificationTypes(ct),
            TimeSpan.FromHours(24),
            logger, ct);
    }
}
