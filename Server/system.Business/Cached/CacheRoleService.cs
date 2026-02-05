using CRMSystem.Business.Abstractions;
using CRMSystem.Business.Extensions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Cached;

public class CacheRoleService(
    IRoleService decorated,
    IDistributedCache distributed,
    ILogger<CacheRoleService> logger) : IRoleService
{
    private const string CACHE_KEY = $"Dict_{nameof(CacheRoleService)}";

    public async Task<List<RoleItem>> GetRoles(CancellationToken ct)
    {
        return await distributed.GetOrCreateAsync(
            CACHE_KEY,
            () => decorated.GetRoles(ct),
            TimeSpan.FromHours(24),
            logger, ct) ?? [];
    }
}
