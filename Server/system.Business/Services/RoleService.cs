using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Services;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;
    private readonly ILogger<RoleService> _logger;

    public RoleService(
        IRoleRepository roleRepository,
        ILogger<RoleService> logger)
    {
        _roleRepository = roleRepository;
        _logger = logger;
    }

    public async Task<List<RoleItem>> GetRoles(CancellationToken ct)
    {
        _logger.LogInformation("Getting roles start");

        var roles = await _roleRepository.Get(ct);

        _logger.LogInformation("Getting roles success");

        return roles;
    }
}
