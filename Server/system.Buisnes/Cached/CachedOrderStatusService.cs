using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Buisnes.Extensions;
using CRMSystem.Core.DTOs;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Buisnes.Cached;

public class CachedOrderStatusService : IOrderStatusService
{
    private readonly IOrderStatusService _decorated;
    private readonly IDistributedCache _distributed;
    private readonly ILogger<CachedOrderStatusService> _logger;

    private const string CACHE_KEY = $"Dict_{nameof(CachedOrderStatusService)}";

    public CachedOrderStatusService(
        IOrderStatusService decorated,
        IDistributedCache distributed,
        ILogger<CachedOrderStatusService> logger)
    {
        _decorated = decorated;
        _distributed = distributed;
        _logger = logger;
    }

    public async Task<List<OrderStatusItem>> GetOrderStatuses()
    {
        return await _distributed.GetOrCreateAsync(
            CACHE_KEY,
            () => _decorated.GetOrderStatuses(),
            TimeSpan.FromHours(24),
            _logger) ?? new List<OrderStatusItem>();
    }
}
