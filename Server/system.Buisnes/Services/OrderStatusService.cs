using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs;
using CRMSystem.DataAccess.Repositories;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Buisnes.Services;

public class OrderStatusService : IOrderStatusService
{
    private readonly IOrderStatusRepository _orderStatusRepository;
    private readonly ILogger<OrderStatusService> _logger;

    public OrderStatusService(
        IOrderStatusRepository orderStatusRepository,
        ILogger<OrderStatusService> logger)
    {
        _orderStatusRepository = orderStatusRepository;
        _logger = logger;
    }

    public async Task<List<OrderStatusItem>> GetOrderStatuses()
    {
        _logger.LogInformation("Getting order priorities start");

        var statuses = await _orderStatusRepository.Get();

        _logger.LogInformation("Getting order priorities succes");

        return statuses;
    }
}
