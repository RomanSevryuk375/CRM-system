using CRMSystem.Core.Models;

namespace CRMSystem.Buisnes.Services
{
    public interface IWorkTypepService
    {
        Task<int> CreateWorkType(WorkType workType);
        Task<int> DeleteWorkType(int id);
        Task<List<WorkType>> GetWorkType();
        Task<int> UpdateWorkType(int id, string title, string category, string description, decimal standardTime);
    }
}