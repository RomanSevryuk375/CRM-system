using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.ProjectionModels.OrderPriority;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Services;

public class OrderPriorityService(
    IOrderPriorityRepository orderPriorityRepository,
    ILogger<OrderPriorityService> logger) : IOrderPriorityService
{
    public async Task<OrderPriorityItem> GetOrderPriorityById(int id, CancellationToken ct)
    {
        return await orderPriorityRepository.GetById(id, ct)
               ?? throw new NotFoundException($"OrderPriority {id} not found");
    }
    
    public async Task<List<OrderPriorityItem>> GetPriorities(CancellationToken ct)
    {
        logger.LogInformation("Getting order priorities start");

        var priorities = await orderPriorityRepository.Get(ct);

        logger.LogInformation("Getting order priorities success");

        return priorities;
    }
}
