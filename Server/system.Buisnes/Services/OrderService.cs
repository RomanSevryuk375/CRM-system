using CRMSystem.Core.DTOs.Order;
using CRMSystem.Core.Enums;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Buisnes.Services;

public class OrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderStatusRepository _orderStatusRepository;
    private readonly ICarRepository _carRepository;
    private readonly IOrderPriorityRepository _orderPriorityRepository;
    private readonly ILogger<OrderService> _logger;

    public OrderService(
        IOrderRepository orderRepository,
        IOrderStatusRepository orderStatusRepository,
        ICarRepository carRepository,
        IOrderPriorityRepository orderPriorityRepository,
        ILogger<OrderService> logger)
    {
        _orderRepository = orderRepository;
        _orderStatusRepository = orderStatusRepository;
        _carRepository = carRepository;
        _orderPriorityRepository = orderPriorityRepository;
        _logger = logger;
    }

    public async Task<List<OrderItem>> GetPagedOrders(OrderFilter filter)
    {
        _logger.LogInformation("Getting orders start");

        var orders = await _orderRepository.GetPaged(filter);

        return orders;
    }

    public async Task<int> GetcountOrders(OrderFilter filter)
    {
        _logger.LogInformation("Getting count orders start");

        var count = await _orderRepository.GetCount(filter);

        return count;
    }

    public async Task<long> CreateOrder(Order order)
    {
        _logger.LogInformation("Creating orders start");

        if (!await _carRepository.Exists(order.CarId))
        {
            _logger.LogError("Car {CarId} not found", order.CarId);
            throw new NotFoundException($"Car {order.CarId} not found");
        }

        if(!await _orderPriorityRepository.Exists((int)order.PriorityId))
        {
            _logger.LogInformation("Priority {priorityId} not found", (int)order.PriorityId);
            throw new NotFoundException($"Priority {order.PriorityId} not found");
        }

        if(!await _orderStatusRepository.Exists((int)order.StatusId))
        {
            _logger.LogInformation("Status{statusId} not found", order.StatusId);
            throw new NotFoundException($"Status {order.StatusId} not found");
        }

        var Id = await _orderRepository.Create(order);

        return Id;
    }

    public async Task<long> UpdateOrder(long id, OrderPriorityEnum? priorityId)
    {
        _logger.LogInformation("Updating orders start");

        if (priorityId.HasValue && !await _orderPriorityRepository.Exists((int)priorityId))
        {
            _logger.LogInformation("Priority {priorityId} not found", (int)priorityId);
            throw new NotFoundException($"Priority {priorityId} not found");
        }

        var Id = await _orderRepository.Update(id, priorityId);

        return Id;
    }

    public async Task<long> DeleteOrder(long id)
    {
        _logger.LogInformation("Deleting orders start");

        var Id = await _orderRepository.Delete(id);

        return Id;
    }

    public async Task<long> CloseOrder(long id)
    {
        _logger.LogInformation("Closing orders start");

        var Id = await _orderRepository.Close(id);

        return Id;
    }

    public async Task<long> CompliteOrder(long id)
    {
        _logger.LogInformation("Closing orders start");

        var Id = await _orderRepository.Complite(id);

        return Id;
    }
}
