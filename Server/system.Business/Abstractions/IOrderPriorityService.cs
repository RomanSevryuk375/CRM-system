using CRMSystem.Core.ProjectionModels;
using CRMSystem.Core.ProjectionModels.OrderPriority;

namespace CRMSystem.Business.Abstractions;

public interface IOrderPriorityService
{
    Task<List<OrderPriorityItem>> GetPriorities(CancellationToken ct);
}