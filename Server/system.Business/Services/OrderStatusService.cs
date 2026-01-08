using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Services;

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

    public async Task<List<OrderStatusItem>> GetOrderStatuses(CancellationToken ct)
    {
        _logger.LogInformation("Getting order priorities start");

        var statuses = await _orderStatusRepository.Get(ct);

        _logger.LogInformation("Getting order priorities success");

        return statuses;
    }
}
