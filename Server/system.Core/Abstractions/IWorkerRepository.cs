using CRMSystem.Core.DTOs.Worker;
using CRMSystem.Core.Models;

namespace CRMSystem.DataAccess.Repositories;

public interface IWorkerRepository
{
    Task<int> Create(Worker worker);
    Task<int> Delete(int id);
    Task<int> GetCount(WorkerFilter filter);
    Task<List<WorkerItem>> GetPaged(WorkerFilter filter);
    Task<WorkerItem?> GetById(int id);
    Task<int> Update(int id, WorkerUpdateModel model);
    Task<bool> Exists(int id);
}