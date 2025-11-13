using CRMSystem.Core.Models;

namespace CRMSystem.Buisnes.Services
{
    public interface IWorkTypepService
    {
        Task<int> CreateWorkType(WorkType workType);
        Task<int> DeleteWorkType(int id);
        Task<List<WorkType>> GetPagedWorkType(int page, int limit);
        Task<int> GetWorkTypeCount();
        Task<int> UpdateWorkType(int id, string title, string category, string description, decimal standardTime);
    }
}