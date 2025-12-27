using CRMSystem.Core.DTOs.Work;
using CRMSystem.Core.Models;

namespace CRMSystem.Buisnes.Abstractions;

public interface IWorkService
{
    Task<long> CreateWork(Work work);
    Task<long> DeleteWork(long id);
    Task<int> GetCountWork();
    Task<List<WorkItem>> GetPagedWork(WorkFilter filter);
    Task<long> UpdateWork(long id, WorkUpdateModel model);
}