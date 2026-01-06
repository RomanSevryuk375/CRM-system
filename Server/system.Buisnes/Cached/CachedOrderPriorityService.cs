using CRMSystem.Business.Abstractions;
using CRMSystem.Business.Extensions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Cached;

public class CachedOrderPriorityService : IOrderPriorityService
{
    private readonly IOrderPriorityService _decorated;
    private readonly IDistributedCache _distributed;
    private readonly ILogger<CachedOrderPriorityService> _logger;

    private const string CACHE_KEY = $"Dict_{nameof(CachedOrderPriorityService)}";

    public CachedOrderPriorityService(
        IOrderPriorityService decorated,
        IDistributedCache distributed,
        ILogger<CachedOrderPriorityService> logger)
    {
        _decorated = decorated;
        _distributed = distributed;
        _logger = logger;
    }
    public async Task<List<OrderPriorityItem>> GetPriorities()
    {
        return await _distributed.GetOrCreateAsync(
            CACHE_KEY,
            () => _decorated.GetPriorities(),
            TimeSpan.FromHours(24),
            _logger) ?? [];
    }
}
