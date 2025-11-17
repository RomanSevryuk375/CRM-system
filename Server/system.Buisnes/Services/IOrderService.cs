using CRMSystem.Buisnes.DTOs;
using CRMSystem.Core.Models;

namespace CRMSystem.Buisnes.Services
{
    public interface IOrderService
    {
        Task<List<Order>> GetPagedOrders(int page, int limit);
        Task<int> GetCountOrders();
        Task<List<OrderWithInfoDto>> GetOrderWithInfo(int page, int limit);
        Task<List<OrderWithInfoDto>> GetPagedUserOrders(int userId, int page, int limit);
        Task<int> GetCountUserOrders(int userId);
        Task<List<OrderWithInfoDto>> GetWorkerOrders(int userId);
        Task<int> UpdateOrder(int id, int statusId, int carId, DateTime date, string priority);
        Task<int> CreateOrder(Order order);
        Task<int> DeleteOrder(int id);
    }
}