using CRMSystem.Buisnes.DTOs;
using CRMSystem.Core.Models;
using System.Threading.Tasks;

namespace CRMSystem.Buisnes.Services
{
    public interface IWorkService
    {
        Task<List<Work>> GetPagedWork(int page, int limit);
        Task<int> GetCountWork();
        Task<List<Work>> GetPagedByWorkerId(List<int> workerId, int page, int limit);
        Task<List<WorkWithInfoDto>> GetPagedInWorkWorks(int userId, int page, int limit);
        Task<int> GetCoutInWorkWorks(int userId);
        Task<List<WorkWithInfoDto>> GetWorkWithInfo();
        Task<List<WorkWithInfoDto>> GetPagedWorkWithInfo(int page, int limit);
        Task<int> CreateWork(Work work);
        Task<int> UpdateWork(int id, int orderId, int jobId, int workerId, decimal timeSpent, int statusId);
        Task<int> DeleteWork(int id);
    }
}