using CRMSystem.Core.ProjectionModels.WorkInOrder;
using Shared.Filters;

namespace CRMSystem.Business.Abstractions;

public interface IWorkInOrderService
{
    Task<long> CreateWiO(WorkInOrderCreateModel createModel, CancellationToken ct);
    Task<long> DeleteWio(long id, CancellationToken ct);
    Task<int> GetCountWiO(WorkInOrderFilter filter, CancellationToken ct);
    Task<List<WorkInOrderItem>> GetPagedWiO(WorkInOrderFilter filter, CancellationToken ct);
    Task<List<WorkInOrderItem>> GetWiOByOrderId(long orderId, CancellationToken ct);
    Task<WorkInOrderItem> GetWorkInOrderById(long id, CancellationToken ct);
    Task<long> UpdateWiO(long id, WorkInOrderUpdateModel model, CancellationToken ct);
}