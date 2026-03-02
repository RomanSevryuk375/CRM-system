using CRMSystem.Core.ProjectionModels.Role;

namespace CRMSystem.Business.Abstractions;

public interface IRoleService
{
    Task<List<RoleItem>> GetRoles(CancellationToken ct);
    Task<RoleItem> GetRoleById(int id, CancellationToken ct);
}