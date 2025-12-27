using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Buisnes.Extensions;
using CRMSystem.Core.DTOs;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Buisnes.Cached;

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

    public async Task<List<RoleItem>> GetRoles()
    {
        return await _distributed.GetOrCreateAsync(
            CACHE_KEY,
            () => _decorated.GetRoles(),
            TimeSpan.FromHours(24),
            _logger) ?? new List<RoleItem>();
    }
}
