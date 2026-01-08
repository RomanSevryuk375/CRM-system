using CRMSystem.Core.ProjectionModels.WorkInOrder;
using CRMSystem.Core.Models;

namespace CRMSystem.Core.Abstractions;

public interface IWorkInOrderRepository
{
    Task<long> Create(WorkInOrder workInOrder, CancellationToken ct);
    Task<long> Delete(long id, CancellationToken ct);
    Task<int> GetCount(WorkInOrderFilter filter, CancellationToken ct);
    Task<List<WorkInOrderItem>> GetPaged(WorkInOrderFilter filter, CancellationToken ct);
    Task<long> Update(long id, WorkInOrderUpdateModel model, CancellationToken ct);
    Task<List<WorkInOrderItem>> GetByOrderId(long orderId, CancellationToken ct);
}