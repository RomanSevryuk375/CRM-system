using CRMSystem.Core.ProjectionModels.Role;

namespace CRMSystem.Core.Abstractions;

public interface IRoleRepository
{
    Task<RoleItem?> GetById(int id, CancellationToken ct);
    Task<List<RoleItem>> Get(CancellationToken ct);
}