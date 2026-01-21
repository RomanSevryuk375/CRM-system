using CRMSystem.Core.ProjectionModels.WorkInOrder;
using CRMSystem.Core.Models;
using Shared.Filters;

namespace CRMSystem.Business.Abstractions;

public interface IWorkInOrderService
{
    Task<long> CreateWiO(WorkInOrder workInOrder, CancellationToken ct);
    Task<long> DeleteWIO(long id, CancellationToken ct);
    Task<int> GetCountWiO(WorkInOrderFilter filter, CancellationToken ct);
    Task<List<WorkInOrderItem>> GetPagedWiO(WorkInOrderFilter filter, CancellationToken ct);
    Task<List<WorkInOrderItem>> GetWiOByOrderId(long orderId, CancellationToken ct);
    Task<long> UpdateWiO(long id, WorkInOrderUpdateModel model, CancellationToken ct);
}