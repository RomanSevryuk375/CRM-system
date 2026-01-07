using CRMSystem.Business.Abstractions;
using CRMSystem.Business.Extensions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Cached;

public class CachedWorkProposalStatusService : IWorkProposalStatusService
{
    private readonly IWorkProposalStatusService _decorated;
    private readonly IDistributedCache _distributed;
    private readonly ILogger<CachedWorkProposalStatusService> _logger;

    private const string CACHE_KEY = $"Dict_{nameof(CachedWorkProposalStatusService)}";

    public CachedWorkProposalStatusService(
        IWorkProposalStatusService decorated,
        IDistributedCache distributed,
        ILogger<CachedWorkProposalStatusService> logger)
    {
        _decorated = decorated;
        _distributed = distributed;
        _logger = logger;
    }
    public async Task<List<WorkProposalStatusItem>> GetProposalStatuses()
    {
        return await _distributed.GetOrCreateAsync(
            CACHE_KEY,
            () => _decorated.GetProposalStatuses(),
            TimeSpan.FromHours(24),
            _logger) ?? [];
    }
}
