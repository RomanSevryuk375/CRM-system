using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using CRMSystem.Business.Extensions;

namespace CRMSystem.Business.Cached;

public class CachedNotificationStatusService : INotificationStatusService
{
    private readonly INotificationStatusService _decorated;
    private readonly IDistributedCache _distributed;
    private readonly ILogger<CachedNotificationStatusService> _logger;

    private const string CACHE_KEY = $"Dict_{nameof(CachedNotificationStatusService)}";

    public CachedNotificationStatusService(
        INotificationStatusService decorated,
        IDistributedCache distributed,
        ILogger<CachedNotificationStatusService> logger)
    {
        _decorated = decorated;
        _distributed = distributed;
        _logger = logger;
    }
    public async Task<List<NotificationStatusItem>> GetNotificationStatuses(CancellationToken ct)
    {
        return await _distributed.GetOrCreateAsync(
            CACHE_KEY,
            () => _decorated.GetNotificationStatuses(ct),
            TimeSpan.FromHours(24),
            _logger, ct) ?? [];
    }
}
