using CRMSystem.Core.ProjectionModels.Worker;
using CRMSystem.Core.Models;
using Shared.Filters;

namespace CRMSystem.Business.Abstractions;

public interface IWorkerService
{
    Task<int> CreateWorker(Worker worker, CancellationToken ct);
    Task<int> CreateWorkerWithUser(Worker worker, User user, CancellationToken ct);
    Task<int> DeleteWorker(int id, CancellationToken ct);
    Task<int> GetCountWorkers(WorkerFilter filter, CancellationToken ct);
    Task<List<WorkerItem>> GetPagedWorkers(WorkerFilter filter, CancellationToken ct);
    Task<WorkerItem> GetWorkerById(int id, CancellationToken ct);
    Task<int> UpdateWorker(int id, WorkerUpdateModel model, CancellationToken ct);
}