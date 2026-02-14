using CRMSystem.Business.Abstractions;
using CRMSystem.Business.Extensions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Cached;

public class CachedCarStatusService(
    ICarStatusService decorated,
    IDistributedCache distributed,
    ILogger<CachedCarStatusService> logger) : ICarStatusService
{
    private const string CACHE_KEY = $"Dict_{nameof(CachedCarStatusService)}";

    public async Task<List<CarStatusItem>> GetCarStatuses(CancellationToken ct)
    {
        return await distributed.GetOrCreateAsync(
            CACHE_KEY,
            () => decorated.GetCarStatuses(ct),
            TimeSpan.FromHours(24),
            logger, ct);
    }
}
