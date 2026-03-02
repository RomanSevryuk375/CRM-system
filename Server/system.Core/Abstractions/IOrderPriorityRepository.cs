using CRMSystem.Core.ProjectionModels.OrderPriority;

namespace CRMSystem.Core.Abstractions;

public interface IOrderPriorityRepository
{
    Task<List<OrderPriorityItem>> Get(CancellationToken ct);
    Task<OrderPriorityItem?> GetById(int id, CancellationToken ct);
    Task<bool> Exists(int id, CancellationToken ct);
}