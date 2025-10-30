using CRMSystem.Core.Models;

namespace CRMSystem.DataAccess.Repositories
{
    public interface IWorkRepository
    {
        Task<int> Create(Work work);
        Task<int> Delete(int id);
        Task<List<Work>> Get();
        Task<int> Update(int id, int? orderId, int? jobId, int? workerId, decimal? timeSpent, int? statusId);
    }
}