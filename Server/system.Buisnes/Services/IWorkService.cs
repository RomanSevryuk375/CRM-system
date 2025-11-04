using CRMSystem.Buisnes.DTOs;
using CRMSystem.Core.Models;

namespace CRMSystem.Buisnes.Services
{
    public interface IWorkService
    {
        Task<int> CreateWork(Work work);
        Task<List<Work>> GetWork();
        Task<List<WorkWithInfoDto>> GetWorkWithInfo();
        Task<int> UpdateWork(int id, int orderId, int jobId, int workerId, decimal timeSpent, int statusId);
        Task<int> DeleteWork(int id);
    }
}