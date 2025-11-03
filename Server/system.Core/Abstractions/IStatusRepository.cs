using CRMSystem.Core.Models;

namespace CRMSystem.DataAccess.Repositories
{
    public interface IStatusRepository
    {
        Task<List<Status>> Get();
    }
}