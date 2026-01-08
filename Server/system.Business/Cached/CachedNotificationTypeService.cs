using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using CRMSystem.Business.Extensions;

namespace CRMSystem.Business.Cached;

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
    public async Task<List<NotificationTypeItem>> GetNotificationTypes(CancellationToken ct)
    {
        return await _distributed.GetOrCreateAsync(
            CACHE_KEY,
            () => _decorated.GetNotificationTypes(ct),
            TimeSpan.FromHours(24),
            _logger, ct) ?? [];
    }
}
