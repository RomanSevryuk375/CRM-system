using CRMSystem.Core.ProjectionModels.Order;
using CRMSystem.Core.Enums;
using CRMSystem.Core.Models;

namespace CRMSystem.Business.Abstractions;

public interface IOrderService
{
    Task<long> CloseOrder(long id, CancellationToken ct);
    Task<long> CompleteOrder(long id, CancellationToken ct);
    Task<long> CreateOrder(Order order, CancellationToken ct);
    Task<long> CreateOrderWithBill(Order order, Bill bill, CancellationToken ct);
    Task<long> DeleteOrder(long id, CancellationToken ct);
    Task<int> GetCountOrders(OrderFilter filter, CancellationToken ct);
    Task<List<OrderItem>> GetPagedOrders(OrderFilter filter, CancellationToken ct);
    Task<long> UpdateOrder(long id, OrderPriorityEnum? priorityId, CancellationToken ct);
}