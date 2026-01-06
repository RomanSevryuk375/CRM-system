using CRMSystem.Core.ProjectionModels;

namespace CRMSystem.Core.Abstractions;

public interface IRoleRepository
{
    Task<List<RoleItem>> Get();
}