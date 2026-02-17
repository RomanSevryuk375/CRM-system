using CRMSystem.Core.ProjectionModels;
using CRMSystem.Core.ProjectionModels.WorkInOrderStatus;

namespace CRMSystem.Core.Abstractions;

public interface IWorkInOrderStatusRepository
{
    Task<List<WorkInOrderStatusItem>> Get(CancellationToken ct);
    Task<bool> Exists(int id, CancellationToken ct);
}