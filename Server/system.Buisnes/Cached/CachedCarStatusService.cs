using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Buisnes.Extensions;
using CRMSystem.Core.DTOs;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Buisnes.Cached;

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
    public async Task<List<CarStatusItem>> GetCarStatuses()
    {
        return await _distributed.GetOrCreateAsync(
            CACHE_KEY,
            () => _decorated.GetCarStatuses(),
            TimeSpan.FromHours(24),
            _logger) ?? new List<CarStatusItem>();
    }
}
