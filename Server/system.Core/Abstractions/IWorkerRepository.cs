using CRMSystem.Core.ProjectionModels.Worker;
using CRMSystem.Core.Models;

namespace CRMSystem.Core.Abstractions;

public interface IWorkerRepository
{
    Task<int> Create(Worker worker, CancellationToken ct);
    Task<int> Delete(int id, CancellationToken ct);
    Task<int> GetCount(WorkerFilter filter, CancellationToken ct);
    Task<List<WorkerItem>> GetPaged(WorkerFilter filter, CancellationToken ct);
    Task<WorkerItem?> GetById(int id, CancellationToken ct);
    Task<int> Update(int id, WorkerUpdateModel model, CancellationToken ct);
    Task<bool> Exists(int id, CancellationToken ct);
}