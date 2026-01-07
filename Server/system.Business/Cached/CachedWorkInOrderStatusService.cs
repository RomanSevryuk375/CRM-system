using CRMSystem.Business.Abstractions;
using CRMSystem.Business.Extensions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Cached;

public class CachedWorkInOrderStatusService : IWorkInOrderStatusService
{
    private readonly IWorkInOrderStatusService _decorated;
    private readonly IDistributedCache _distributed;
    private readonly ILogger<CachedWorkInOrderStatusService> _logger;

    private const string CACHE_KEY = $"Dict_{nameof(CachedWorkInOrderStatusService)}";

    public CachedWorkInOrderStatusService(
        IWorkInOrderStatusService decorated,
        IDistributedCache distributed,
        ILogger<CachedWorkInOrderStatusService> logger)
    {
        _decorated = decorated;
        _distributed = distributed;
        _logger = logger;
    }
    public async Task<List<WorkInOrderStatusItem>> GetWiOStatuses()
    {
        return await _distributed.GetOrCreateAsync(
            CACHE_KEY,
            () => _decorated.GetWiOStatuses(),
            TimeSpan.FromHours(24),
            _logger) ?? [];
    }
}
