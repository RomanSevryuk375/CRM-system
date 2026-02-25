using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.ProjectionModels.Role;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Services;

public class RoleService(
    IRoleRepository roleRepository,
    ILogger<RoleService> logger) : IRoleService
{
    public async Task<RoleItem> GetRoleById(int id, CancellationToken ct)
    {
        return await roleRepository.GetById(id, ct)
               ?? throw new NotFoundException($"Role {id} not found");
    }
    
    public async Task<List<RoleItem>> GetRoles(CancellationToken ct)
    {
        logger.LogInformation("Getting roles start");

        var roles = await roleRepository.Get(ct);

        logger.LogInformation("Getting roles success");

        return roles;
    }
}
