using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs;
using CRMSystem.DataAccess.Repositories;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Buisnes.Services;

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

    public async Task<List<OrderPriorityItem>> GetPrioritys()
    {
        _logger.LogInformation("Getting order priorities start");

        var prioritys = await _orderPriorityRepository.Get();

        _logger.LogInformation("Getting order priorities succes");

        return prioritys;
    }
}
