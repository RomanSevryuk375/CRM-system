using CRMSystem.Core.Models;

namespace CRMSystem.DataAccess.Repositories
{
    public interface IWorkTypeRepository
    {
        Task<int> Create(WorkType workType);
        Task<int> Delete(int id);
        Task<List<WorkType>> Get();
        Task<int> GetCount();
        Task<int> Update(int id, string title, string category, string description, decimal? standardTime);
    }
}