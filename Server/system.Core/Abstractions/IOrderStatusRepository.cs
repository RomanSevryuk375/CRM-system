using CRMSystem.Core.ProjectionModels.OrderStatus;

namespace CRMSystem.Core.Abstractions;

public interface IOrderStatusRepository
{
    Task<List<OrderStatusItem>> Get(CancellationToken ct);
    Task<OrderStatusItem?> GetById(int id, CancellationToken ct);
    Task<bool> Exists(int id, CancellationToken ct);
}