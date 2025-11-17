using CRMSystem.Core.Models;

namespace CRMSystem.DataAccess.Repositories
{
    public interface IWorkRepository
    {
        Task<List<Work>> Get();
        Task<int> GetCount();
        Task<List<Work>> GetByWorkerId(List<int> workerIds);
        Task<int> GetCountByWorkerId(List<int> workerIds);
        Task<int> Create(Work work);
        Task<int> Update(int id, int? orderId, int? jobId, int? workerId, decimal? timeSpent, int? statusId);
        Task<int> Delete(int id);
    }
}