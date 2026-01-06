using CRMSystem.Core.ProjectionModels.Work;
using CRMSystem.Core.Models;

namespace CRMSystem.Core.Abstractions;

public interface IWorkRepository
{
    Task<long> Create(Work work);
    Task<long> Delete(long id);
    Task<int> GetCount();
    Task<List<WorkItem>> GetPaged(WorkFilter filter);
    Task<long> Update(long id, WorkUpdateModel model);
    Task<WorkItem?> GetById(long id);
    Task<bool> Exists(long id);
}