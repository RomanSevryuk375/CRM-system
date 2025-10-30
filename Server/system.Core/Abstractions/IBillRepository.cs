using CRMSystem.Core.Models;

namespace CRMSystem.DataAccess.Repositories
{
    public interface IBillRepository
    {
        Task<List<Bill>> Get();
        Task<int> Update(int id, int? orderId, int? statusId, DateTime? date, decimal? amount, DateTime? actualBillDate);
    }
}