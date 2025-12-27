using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Buisnes.Extensions;
using CRMSystem.Core.DTOs;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Buisnes.Cached;

public class CachedNotificationTypeService : INotificationTypeService
{
    private readonly INotificationTypeService _decorated;
    private readonly IDistributedCache _distributed;
    private readonly ILogger<CachedNotificationTypeService> _logger;

    private const string CACHE_KEY = $"Dict_{nameof(CachedNotificationTypeService)}";

    public CachedNotificationTypeService(
        INotificationTypeService decorated,
        IDistributedCache distributed,
        ILogger<CachedNotificationTypeService> logger)
    {
        _decorated = decorated;
        _distributed = distributed;
        _logger = logger;
    }
    public async Task<List<NotificationTypeItem>> GetNotificationTypes()
    {
        return await _distributed.GetOrCreateAsync(
            CACHE_KEY,
            () => _decorated.GetNotificationTypes(),
            TimeSpan.FromHours(24),
            _logger) ?? new List<NotificationTypeItem>();
    }
}
