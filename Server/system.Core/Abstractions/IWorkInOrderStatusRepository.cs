using CRMSystem.Core.DTOs;

namespace CRMSystem.DataAccess.Repositories
{
    public interface IWorkInOrderStatusRepository
    {
        Task<List<WorkInOrderStatusItem>> Get();
    }
}