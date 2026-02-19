using CRMSystem.Core.ProjectionModels.Worker;
using CRMSystem.Core.ProjectionModels.User;
using Shared.Filters;

namespace CRMSystem.Business.Abstractions;

public interface IWorkerService
{
    Task<int> CreateWorker(WorkerCreateModel createModel, CancellationToken ct);
    Task<int> CreateWorkerWithUser(
        WorkerCreateModel workerCreateModel,
        UserCreateModel userCreateModel,
        CancellationToken ct);
    Task<int> DeleteWorker(int id, CancellationToken ct);
    Task<int> GetCountWorkers(WorkerFilter filter, CancellationToken ct);
    Task<List<WorkerItem>> GetPagedWorkers(WorkerFilter filter, CancellationToken ct);
    Task<WorkerItem> GetWorkerById(int id, CancellationToken ct);
    Task<int> UpdateWorker(int id, WorkerUpdateModel model, CancellationToken ct);
}