using CRMSystem.Buisnes.DTOs;
using CRMSystem.Core.Models;

namespace CRMSystem.Buisnes.Services
{
    public interface IOrderService
    {
        Task<int> CreateOrder(Order order);
        Task<int> DeleteOrder(int id);
        Task<List<Order>> GetOrders();
        Task<List<OrderWithInfoDto>> GetOrderWithInfo();
        Task<List<OrderWithInfoDto>> GetUserOrders(int userId);
        Task<List<OrderWithInfoDto>> GetWorkerOrders(int userId);
        Task<int> UpdateOrder(int id, int statusId, int carId, DateTime date, string priority);
    }
}