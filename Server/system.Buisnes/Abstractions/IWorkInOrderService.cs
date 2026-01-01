using CRMSystem.Core.DTOs.WorkInOrder;
using CRMSystem.Core.Models;

namespace CRMSystem.Buisnes.Abstractions;

public interface IWorkInOrderService
{
    Task<long> CreateWiO(WorkInOrder workInOrder);
    Task<long> DeleteWIO(long id);
    Task<int> GetCountWiO(WorkInOrderFilter filter);
    Task<List<WorkInOrderItem>> GetPagedWiO(WorkInOrderFilter filter);
    Task<List<WorkInOrderItem>> GetWiOByOrderId(long orderId);
    Task<long> UpdateWiO(long id, WorkInOrderUpdateModel model);
}