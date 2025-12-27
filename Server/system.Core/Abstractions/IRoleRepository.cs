using CRMSystem.Core.DTOs;

namespace CRMSystem.DataAccess.Repositories
{
    public interface IRoleRepository
    {
        Task<List<RoleItem>> Get();
    }
}