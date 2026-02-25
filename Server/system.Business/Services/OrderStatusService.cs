using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.ProjectionModels.OrderStatus;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Services;

public class OrderStatusService(
    IOrderStatusRepository orderStatusRepository,
    ILogger<OrderStatusService> logger) : IOrderStatusService
{
    public async Task<OrderStatusItem> GetOrderStatusById(int id, CancellationToken ct)
    {
        return await orderStatusRepository.GetById(id, ct)
               ?? throw new NotFoundException($"OrderStatus {id} not found");
    }
    
    public async Task<List<OrderStatusItem>> GetOrderStatuses(CancellationToken ct)
    {
        logger.LogInformation("Getting order priorities start");

        var statuses = await orderStatusRepository.Get(ct);

        logger.LogInformation("Getting order priorities success");

        return statuses;
    }
}
