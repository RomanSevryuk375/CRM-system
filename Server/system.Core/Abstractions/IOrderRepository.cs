using CRMSystem.Core.Models;

namespace CRMSystem.DataAccess.Repositories
{
    public interface IOrderRepository
    {
        Task<int> Create(Order order);
        Task<int> Delete(int id);
        Task<List<Order>> Get();
        Task<int> Update(int id, int? statusId, int? carId, DateTime? date, string priority);
    }
}