using CRMSystem.Core.DTOs.Work;
using CRMSystem.Core.Models;

namespace CRMSystem.DataAccess.Repositories
{
    public interface IWorkRepository
    {
        Task<long> Create(Work work);
        Task<long> Delete(long id);
        Task<int> GetCount();
        Task<List<WorkItem>> GetPaged(WorkFilter filter);
        Task<long> Update(long id, WorkUpdateModel model);
    }
}