using CRMSystem.Core.ProjectionModels.Role;

namespace CRMSystem.Core.Abstractions;

public interface IRoleRepository
{
    Task<List<RoleItem>> Get(CancellationToken ct);
}