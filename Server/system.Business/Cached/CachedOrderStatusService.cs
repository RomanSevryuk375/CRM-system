using CRMSystem.Business.Abstractions;
using CRMSystem.Business.Extensions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Cached;

public class CachedOrderStatusService(
    IOrderStatusService decorated,
    IDistributedCache distributed,
    ILogger<CachedOrderStatusService> logger) : IOrderStatusService
{
    private const string CACHE_KEY = $"Dict_{nameof(CachedOrderStatusService)}";

    public async Task<List<OrderStatusItem>> GetOrderStatuses(CancellationToken ct)
    {
        return await distributed.GetOrCreateAsync(
            CACHE_KEY,
            () => decorated.GetOrderStatuses(ct),
            TimeSpan.FromHours(24),
            logger, ct) ?? [];
    }
}
