using CRMSystem.Business.Abstractions;
using CRMSystem.Business.Extensions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Cached;

public class CachedWorkProposalStatusService(
    IWorkProposalStatusService decorated,
    IDistributedCache distributed,
    ILogger<CachedWorkProposalStatusService> logger) : IWorkProposalStatusService
{
    private const string CACHE_KEY = $"Dict_{nameof(CachedWorkProposalStatusService)}";

    public async Task<List<WorkProposalStatusItem>> GetProposalStatuses(CancellationToken ct)
    {
        return await distributed.GetOrCreateAsync(
            CACHE_KEY,
            () => decorated.GetProposalStatuses(ct),
            TimeSpan.FromHours(24),
            logger, ct) ?? [];
    }
}
