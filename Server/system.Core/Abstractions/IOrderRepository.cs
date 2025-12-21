using CRMSystem.Core.DTOs.Order;
using CRMSystem.Core.Enums;
using CRMSystem.Core.Models;

namespace CRMSystem.DataAccess.Repositories
{
    public interface IOrderRepository
    {
        Task<long> Create(Order order);
        Task<long> Delete(long id);
        Task<int> GetCount(OrderFilter filter);
        Task<List<OrderItem>> GetPaged(OrderFilter filter);
        Task<long> Update(long id, OrderPriorityEnum? priorityId);
        Task<bool> Exists(long id);
        Task<int?> GetStatus(long id);
    }
}