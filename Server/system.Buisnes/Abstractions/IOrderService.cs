using CRMSystem.Core.ProjectionModels.Order;
using CRMSystem.Core.Enums;
using CRMSystem.Core.Models;

namespace CRMSystem.Business.Abstractions;

public interface IOrderService
{
    Task<long> CloseOrder(long id);
    Task<long> CompleteOrder(long id);
    Task<long> CreateOrder(Order order);
    Task<long> CreateOrderWithBill(Order order, Bill bill);
    Task<long> DeleteOrder(long id);
    Task<int> GetCountOrders(OrderFilter filter);
    Task<List<OrderItem>> GetPagedOrders(OrderFilter filter);
    Task<long> UpdateOrder(long id, OrderPriorityEnum? priorityId);
}