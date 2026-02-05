using CRMSystem.Business.Abstractions;
using CRMSystem.Business.Extensions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Cached;

public class CachedWorkInOrderStatusService(
    IWorkInOrderStatusService decorated,
    IDistributedCache distributed,
    ILogger<CachedWorkInOrderStatusService> logger) : IWorkInOrderStatusService
{
    private const string CACHE_KEY = $"Dict_{nameof(CachedWorkInOrderStatusService)}";

    public async Task<List<WorkInOrderStatusItem>> GetWiOStatuses(CancellationToken ct)
    {
        return await distributed.GetOrCreateAsync(
            CACHE_KEY,
            () => decorated.GetWiOStatuses(ct),
            TimeSpan.FromHours(24),
            logger, ct) ?? [];
    }
}
