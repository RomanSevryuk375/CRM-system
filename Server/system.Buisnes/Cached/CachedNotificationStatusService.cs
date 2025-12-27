using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Buisnes.Extensions;
using CRMSystem.Core.DTOs;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Buisnes.Cached;

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
    public async Task<List<NotificationStatusItem>> GetNotificationStatuses()
    {
        return await _distributed.GetOrCreateAsync(
            CACHE_KEY,
            () => _decorated.GetNotificationStatuses(),
            TimeSpan.FromHours(24),
            _logger) ?? new List<NotificationStatusItem>();
    }
}
