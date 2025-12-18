using CRMSystem.Core.DTOs.WorkInOrder;
using CRMSystem.Core.Models;

namespace CRMSystem.DataAccess.Repositories
{
    public interface IWorkInOrderRepository
    {
        Task<long> Create(WorkInOrder workInOrder);
        Task<long> Delete(long id);
        Task<int> GetCount(WorkInOrderFilter filter);
        Task<List<WorkInOrderItem>> GetPaged(WorkInOrderFilter filter);
        Task<long> Update(long id, WorkInOrderUpdateModel model);
    }
}