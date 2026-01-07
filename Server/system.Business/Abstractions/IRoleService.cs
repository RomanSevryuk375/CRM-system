using CRMSystem.Core.ProjectionModels;

namespace CRMSystem.Business.Abstractions;

public interface IRoleService
{
    Task<List<RoleItem>> GetRoles();
}