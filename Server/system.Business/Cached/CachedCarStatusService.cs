using CRMSystem.Business.Abstractions;
using CRMSystem.Business.Extensions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Cached;

public class CachedCarStatusService : ICarStatusService
{
    private readonly ICarStatusService _decorated;
    private readonly IDistributedCache _distributed;
    private readonly ILogger<CachedCarStatusService> _logger;

    private const string CACHE_KEY = $"Dict_{nameof(CachedCarStatusService)}";

    public CachedCarStatusService(
        ICarStatusService decorated,
        IDistributedCache distributed,
        ILogger<CachedCarStatusService> logger)
    {
        _decorated = decorated;
        _distributed = distributed;
        _logger = logger;
    }
    public async Task<List<CarStatusItem>> GetCarStatuses(CancellationToken ct)
    {
        return await _distributed.GetOrCreateAsync(
            CACHE_KEY,
            () => _decorated.GetCarStatuses(ct),
            TimeSpan.FromHours(24),
            _logger, ct) ?? [];
    }
}
