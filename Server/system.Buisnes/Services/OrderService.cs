using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.Order;
using CRMSystem.Core.Enums;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Repositories;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderStatusRepository _orderStatusRepository;
    private readonly ICarRepository _carRepository;
    private readonly IOrderPriorityRepository _orderPriorityRepository;
    private readonly IBillRepository _billRepository;
    private readonly ILogger<OrderService> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public OrderService(
        IOrderRepository orderRepository,
        IOrderStatusRepository orderStatusRepository,
        ICarRepository carRepository,
        IOrderPriorityRepository orderPriorityRepository,
        IBillRepository billRepository,
        ILogger<OrderService> logger,
        IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _orderStatusRepository = orderStatusRepository;
        _carRepository = carRepository;
        _orderPriorityRepository = orderPriorityRepository;
        _billRepository = billRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<OrderItem>> GetPagedOrders(OrderFilter filter)
    {
        _logger.LogInformation("Getting orders start");

        var orders = await _orderRepository.GetPaged(filter);

        _logger.LogInformation("Getting orders success");

        return orders;
    }

    public async Task<int> GetCountOrders(OrderFilter filter)
    {
        _logger.LogInformation("Getting count orders start");

        var count = await _orderRepository.GetCount(filter);

        _logger.LogInformation("Getting count orders success");

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

        if (!await _orderPriorityRepository.Exists((int)order.PriorityId))
        {
            _logger.LogInformation("Priority {priorityId} not found", (int)order.PriorityId);
            throw new NotFoundException($"Priority {order.PriorityId} not found");
        }

        if (!await _orderStatusRepository.Exists((int)order.StatusId))
        {
            _logger.LogInformation("Status{statusId} not found", order.StatusId);
            throw new NotFoundException($"Status {order.StatusId} not found");
        }

        var Id = await _orderRepository.Create(order);

        _logger.LogInformation("Creating orders success");

        return Id;
    }

    public async Task<long> CreateOrderWithBill(Order order, Bill bill)
    {
        await _unitOfWork.BeginTransactionAsync();

        long orderId;
        try
        {
            _logger.LogInformation("Creating orders start");

            if (!await _carRepository.Exists(order.CarId))
            {
                _logger.LogError("Car {CarId} not found", order.CarId);
                throw new NotFoundException($"Car {order.CarId} not found");
            }

            if (!await _orderPriorityRepository.Exists((int)order.PriorityId))
            {
                _logger.LogInformation("Priority {priorityId} not found", (int)order.PriorityId);
                throw new NotFoundException($"Priority {order.PriorityId} not found");
            }

            if (!await _orderStatusRepository.Exists((int)order.StatusId))
            {
                _logger.LogInformation("Status{statusId} not found", order.StatusId);
                throw new NotFoundException($"Status {order.StatusId} not found");
            }

            orderId = await _orderRepository.Create(order);

            bill.SetOrderId(orderId);
            var newBill = await _billRepository.Create(bill);

            _logger.LogInformation("Creating orders success");

            await _unitOfWork.CommitTransactionAsync();

            return orderId;
        }
        catch (ConflictException ex)
        {
            _logger.LogError(ex, "Transaction failed. Rolling back all changes.");
            
            await _unitOfWork.RollbackAsync();

            throw;
        }
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

        _logger.LogInformation("Updating orders success");

        return Id;
    }

    public async Task<long> DeleteOrder(long id)
    {
        _logger.LogInformation("Deleting orders start");

        var Id = await _orderRepository.Delete(id);

        _logger.LogInformation("Deleting orders success");

        return Id;
    }

    public async Task<long> CloseOrder(long id)
    {
        _logger.LogInformation("Closing orders start");

        if (!await _orderRepository.PossibleToClose(id))
        {
            _logger.LogInformation("Order{orderId} has not paid bill", id);
            throw new ConflictException($"Order{id} has not paid bill");
        }

        var Id = await _orderRepository.Close(id);

        _logger.LogInformation("Closing orders success");

        return Id;
    }

    public async Task<long> CompleteOrder(long id)
    {
        _logger.LogInformation("Closing orders start");

        if (await _orderRepository.PossibleToComplete(id))
        {
            _logger.LogInformation("Order{id} has unfinished works", id);
            throw new ConflictException("Order has unfinished works");
        }

        var Id = await _orderRepository.Complete(id);

        _logger.LogInformation("Closing orders success");

        return Id;
    }
}
