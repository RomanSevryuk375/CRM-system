using CRMSystem.Core.Models;

namespace CRMSystem.DataAccess.Repositories
{
    public interface IBillRepository
    {
        Task<List<Bill>> Get();
        Task<int> GetCount();
        Task<int> GetCountByOrderId(List<int> orderIds);
        Task<List<Bill>> GetByOrderId(List<int> orderIds);
        Task<int> Create(Bill bill);
    }
}