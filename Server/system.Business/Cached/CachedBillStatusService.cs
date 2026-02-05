using CRMSystem.Business.Abstractions;
using CRMSystem.Business.Extensions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Cached;

public class CachedBillStatusService(
    IBillStatusService decorated,
    IDistributedCache distributed,
    ILogger<CachedBillStatusService> logger) : IBillStatusService
{
    private const string CACHE_KEY = $"Dict_{nameof(CachedBillStatusService)}";

    public async Task<List<BillStatusItem>> GetAllBillStatuses(CancellationToken ct)
    {
        return await distributed.GetOrCreateAsync(
            CACHE_KEY,
            () => decorated.GetAllBillStatuses(ct),
            TimeSpan.FromHours(24),
            logger, ct) ?? [];
    }
}
