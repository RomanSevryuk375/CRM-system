using CRMSystem.Core.ProjectionModels.Worker;
using CRMSystem.Core.Models;

namespace CRMSystem.Business.Abstractions;

public interface IWorkerService
{
    Task<int> CreateWorker(Worker worker);
    Task<int> CreateWorkerWithUser(Worker worker, User user);
    Task<int> DeleteWorker(int id);
    Task<int> GetCountWorkers(WorkerFilter filter);
    Task<List<WorkerItem>> GetPagedWorkers(WorkerFilter filter);
    Task<WorkerItem> GetWorkerById(int id);
    Task<int> UpdateWorker(int id, WorkerUpdateModel model);
}