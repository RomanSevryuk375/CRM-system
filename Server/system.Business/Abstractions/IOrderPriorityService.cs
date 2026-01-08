using CRMSystem.Core.ProjectionModels;

namespace CRMSystem.Business.Abstractions;

public interface IOrderPriorityService
{
    Task<List<OrderPriorityItem>> GetPriorities(CancellationToken ct);
}