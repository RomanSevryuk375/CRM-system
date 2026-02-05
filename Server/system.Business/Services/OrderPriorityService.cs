using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Services;

public class OrderPriorityService(
    IOrderPriorityRepository orderPriorityRepository,
    ILogger<OrderPriorityService> logger) : IOrderPriorityService
{
    public async Task<List<OrderPriorityItem>> GetPriorities(CancellationToken ct)
    {
        logger.LogInformation("Getting order priorities start");

        var prioritys = await orderPriorityRepository.Get(ct);

        logger.LogInformation("Getting order priorities success");

        return prioritys;
    }
}
