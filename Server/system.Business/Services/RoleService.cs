using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Services;

public class RoleService(
    IRoleRepository roleRepository,
    ILogger<RoleService> logger) : IRoleService
{
    public async Task<List<RoleItem>> GetRoles(CancellationToken ct)
    {
        logger.LogInformation("Getting roles start");

        var roles = await roleRepository.Get(ct);

        logger.LogInformation("Getting roles success");

        return roles;
    }
}
