using CRMSystem.Core.DTOs;

namespace CRMSystem.Buisnes.Abstractions;

public interface IRoleService
{
    Task<List<RoleItem>> GetRoles();
}