using CRMSystem.Core.Models;

namespace CRMSystem.DataAccess.Repositories
{
    public interface IOrderRepository
    {
        Task<List<Order>> Get();
        Task<List<Order>> GetPaged(int page, int limit);
        Task<int> GetCount();
        Task<List<Order>> GetById(List<int> orderId);
        Task<List<Order>> GetPagedById(List<int> orderIds, int page, int limit);
        Task<int> GetCountById(List<int> orderIds);
        Task<List<Order>> GetByCarId(List<int> carId);
        Task<List<Order>> GetByCarId(int carIds);
        Task<List<Order>> GetPagedByCarId(List<int> carIds, int page, int limit);
        Task<int> GetCountByCarId(List<int> carIds);
        Task<int> Update(int id, int? statusId, int? carId, DateTime? date, string priority);
        Task<int> Create(Order order);
        Task<int> Delete(int id);
    }
}