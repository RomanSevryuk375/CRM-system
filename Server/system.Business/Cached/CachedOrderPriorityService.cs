using CRMSystem.Business.Abstractions;
using CRMSystem.Business.Extensions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Cached;

public class CachedOrderPriorityService(
    IOrderPriorityService decorated,
    IDistributedCache distributed,
    ILogger<CachedOrderPriorityService> logger) : IOrderPriorityService
{
    private const string CACHE_KEY = $"Dict_{nameof(CachedOrderPriorityService)}";

    public async Task<List<OrderPriorityItem>> GetPriorities(CancellationToken ct)
    {
        return await distributed.GetOrCreateAsync(
            CACHE_KEY,
            () => decorated.GetPriorities(ct),
            TimeSpan.FromHours(24),
            logger, ct);
    }
}
