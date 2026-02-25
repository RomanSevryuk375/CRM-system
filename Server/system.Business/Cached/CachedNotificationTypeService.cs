using CRMSystem.Business.Abstractions;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using CRMSystem.Business.Extensions;
using CRMSystem.Core.ProjectionModels.NotificationType;

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

    public async Task<NotificationTypeItem> GetNotificationTypeById(int id, CancellationToken ct)
    {
        return await decorated.GetNotificationTypeById(id, ct);
    }
}
