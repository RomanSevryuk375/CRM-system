using CRMSystem.Core.ProjectionModels.OrderStatus;

namespace CRMSystem.Business.Abstractions;

public interface IOrderStatusService
{
    Task<List<OrderStatusItem>> GetOrderStatuses(CancellationToken ct);
    Task<OrderStatusItem> GetOrderStatusById(int id, CancellationToken ct);
}