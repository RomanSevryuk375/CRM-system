using CRMSystem.Core.Models;

namespace CRMSystem.DataAccess.Repositories
{
    public interface IBillRepository
    {
        Task<List<Bill>> Get();
        Task<List<Bill>> GetPaged(int page, int limit);
        Task<int> GetCount();
        Task<List<Bill>> GetByOrderId(List<int> orderIds);
        Task<List<Bill>> GetPagedByOrderId(List<int> orderIds, int page, int limit);
        Task<int> GetCountByOrderId(List<int> orderIds);
        Task<int> Create(Bill bill);
    }
}