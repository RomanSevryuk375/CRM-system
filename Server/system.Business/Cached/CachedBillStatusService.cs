using CRMSystem.Business.Abstractions;
using CRMSystem.Business.Extensions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Cached;

public class CachedBillStatusService : IBillStatusService
{
    private readonly IBillStatusService _decorated;
    private readonly IDistributedCache _distributed;
    private readonly ILogger<CachedBillStatusService> _logger;

    private const string CACHE_KEY = $"Dict_{nameof(CachedBillStatusService)}";

    public CachedBillStatusService(
        IBillStatusService decorated,
        IDistributedCache distributed,
        ILogger<CachedBillStatusService> logger)
    {
        _decorated = decorated;
        _distributed = distributed;
        _logger = logger;
    }
    public async Task<List<BillStatusItem>> GetAllBillStatuses()
    {
        return await _distributed.GetOrCreateAsync(
            CACHE_KEY,
            () => _decorated.GetAllBillStatuses(),
            TimeSpan.FromHours(24),
            _logger) ?? [];
    }
}
