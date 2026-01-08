using CRMSystem.Core.ProjectionModels;

namespace CRMSystem.Business.Abstractions;

public interface IOrderStatusService
{
    Task<List<OrderStatusItem>> GetOrderStatuses(CancellationToken ct);
}