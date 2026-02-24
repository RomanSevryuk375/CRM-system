using CRMSystem.Core.ProjectionModels.WorkInOrderStatus;

namespace CRMSystem.Business.Abstractions;

public interface IWorkInOrderStatusService
{
    Task<List<WorkInOrderStatusItem>> GetWiOStatuses(CancellationToken ct);
    Task<WorkInOrderStatusItem> GetWorkInOrderStatusById(int id, CancellationToken ct);
}