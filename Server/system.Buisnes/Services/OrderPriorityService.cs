using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels;
using CRMSystem.DataAccess.Repositories;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Services;

public class OrderPriorityService : IOrderPriorityService
{
    private readonly IOrderPriorityRepository _orderPriorityRepository;
    private readonly ILogger<OrderPriorityService> _logger;

    public OrderPriorityService(
        IOrderPriorityRepository orderPriorityRepository,
        ILogger<OrderPriorityService> logger)
    {
        _orderPriorityRepository = orderPriorityRepository;
        _logger = logger;
    }

    public async Task<List<OrderPriorityItem>> GetPriorities()
    {
        _logger.LogInformation("Getting order priorities start");

        var prioritys = await _orderPriorityRepository.Get();

        _logger.LogInformation("Getting order priorities success");

        return prioritys;
    }
}
