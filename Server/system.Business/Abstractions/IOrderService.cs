using CRMSystem.Core.ProjectionModels.Order;
using CRMSystem.Core.ProjectionModels.Bill;
using Shared.Enums;
using Shared.Filters;

namespace CRMSystem.Business.Abstractions;

public interface IOrderService
{
    Task<long> CloseOrder(long id, CancellationToken ct);
    Task<long> CompleteOrder(long id, CancellationToken ct);
    Task<long> CreateOrder(OrderCreateModel createModel, CancellationToken ct);
    Task<long> CreateOrderWithBill(
        OrderCreateModel orderCreateModel, 
        BillCreateModel billCreateModel, 
        CancellationToken ct);
    Task<long> DeleteOrder(long id, CancellationToken ct);
    Task<int> GetCountOrders(OrderFilter filter, CancellationToken ct);
    Task<List<OrderItem>> GetPagedOrders(OrderFilter filter, CancellationToken ct);
    Task<long> UpdateOrder(long id, OrderPriorityEnum? priorityId, CancellationToken ct);
}