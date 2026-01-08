using CRMSystem.Core.ProjectionModels;

namespace CRMSystem.Core.Abstractions;

public interface IOrderPriorityRepository
{
    Task<List<OrderPriorityItem>> Get(CancellationToken ct);
    Task<bool> Exists(int id, CancellationToken ct);
}