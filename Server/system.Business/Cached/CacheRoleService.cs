using CRMSystem.Business.Abstractions;
using CRMSystem.Business.Extensions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Cached;

public class CacheRoleService : IRoleService
{
    private readonly IRoleService _decorated;
    private readonly IDistributedCache _distributed;
    private readonly ILogger<CacheRoleService> _logger;

    private const string CACHE_KEY = $"Dict_{nameof(CacheRoleService)}";

    public CacheRoleService(
        IRoleService decorated,
        IDistributedCache distributed,
        ILogger<CacheRoleService> logger)
    {
        _decorated = decorated;
        _distributed = distributed;
        _logger = logger;
    }

    public async Task<List<RoleItem>> GetRoles(CancellationToken ct)
    {
        return await _distributed.GetOrCreateAsync(
            CACHE_KEY,
            () => _decorated.GetRoles(ct),
            TimeSpan.FromHours(24),
            _logger, ct) ?? [];
    }
}
