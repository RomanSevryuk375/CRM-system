using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.Order;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using Microsoft.Extensions.Logging;
using Shared.Enums;
using Shared.Filters;

namespace CRMSystem.Business.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderStatusRepository _orderStatusRepository;
    private readonly ICarRepository _carRepository;
    private readonly IOrderPriorityRepository _orderPriorityRepository;
    private readonly IBillRepository _billRepository;
    private readonly IUserContext _userContext;
    private readonly ILogger<OrderService> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public OrderService(
        IOrderRepository orderRepository,
        IOrderStatusRepository orderStatusRepository,
        ICarRepository carRepository,
        IOrderPriorityRepository orderPriorityRepository,
        IBillRepository billRepository,
        IUserContext userContext,
        ILogger<OrderService> logger,
        IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _orderStatusRepository = orderStatusRepository;
        _carRepository = carRepository;
        _orderPriorityRepository = orderPriorityRepository;
        _billRepository = billRepository;
        _userContext = userContext;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<OrderItem>> GetPagedOrders(OrderFilter filter, CancellationToken ct)
    {
        _logger.LogInformation("Getting orders start");

        if (_userContext.RoleId == (int)RoleEnum.Worker)
            filter = filter with { WorkerIds = [(int)_userContext.ProfileId] };

        if (_userContext.RoleId == (int)RoleEnum.Client)
            filter = filter with { ClientIds = [_userContext.ProfileId] };

        var orders = await _orderRepository.GetPaged(filter, ct);

        _logger.LogInformation("Getting orders success");

        return orders;
    }

    public async Task<int> GetCountOrders(OrderFilter filter, CancellationToken ct)
    {
        _logger.LogInformation("Getting count orders start");

        var count = await _orderRepository.GetCount(filter, ct);

        _logger.LogInformation("Getting count orders success");

        return count;
    }

    public async Task<long> CreateOrder(Order order, CancellationToken ct)
    {
        _logger.LogInformation("Creating orders start");

        if (!await _carRepository.Exists(order.CarId, ct))
        {
            _logger.LogError("Car {CarId} not found", order.CarId);
            throw new NotFoundException($"Car {order.CarId} not found");
        }

        if (!await _orderPriorityRepository.Exists((int)order.PriorityId, ct))
        {
            _logger.LogInformation("Priority {priorityId} not found", (int)order.PriorityId);
            throw new NotFoundException($"Priority {order.PriorityId} not found");
        }

        if (!await _orderStatusRepository.Exists((int)order.StatusId, ct))
        {
            _logger.LogInformation("Status{statusId} not found", order.StatusId);
            throw new NotFoundException($"Status {order.StatusId} not found");
        }

        var Id = await _orderRepository.Create(order, ct);

        _logger.LogInformation("Creating orders success");

        return Id;
    }

    public async Task<long> CreateOrderWithBill(Order order, Bill bill, CancellationToken ct)
    {
        await _unitOfWork.BeginTransactionAsync(ct);

        long orderId;
        try
        {
            _logger.LogInformation("Creating orders start");

            if (!await _carRepository.Exists(order.CarId, ct))
            {
                _logger.LogError("Car {CarId} not found", order.CarId);
                throw new NotFoundException($"Car {order.CarId} not found");
            }

            if (!await _orderPriorityRepository.Exists((int)order.PriorityId, ct))
            {
                _logger.LogInformation("Priority {priorityId} not found", (int)order.PriorityId);
                throw new NotFoundException($"Priority {order.PriorityId} not found");
            }

            if (!await _orderStatusRepository.Exists((int)order.StatusId, ct))
            {
                _logger.LogInformation("Status{statusId} not found", order.StatusId);
                throw new NotFoundException($"Status {order.StatusId} not found");
            }

            orderId = await _orderRepository.Create(order, ct);

            bill.SetOrderId(orderId);
            var newBill = await _billRepository.Create(bill, ct);

            _logger.LogInformation("Creating orders success");

            await _unitOfWork.CommitTransactionAsync(ct);

            return orderId;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Transaction failed. Rolling back all changes.");
            
            await _unitOfWork.RollbackAsync(ct);

            throw;
        }
    }

    public async Task<long> UpdateOrder(long id, OrderPriorityEnum? priorityId, CancellationToken ct)
    {
        _logger.LogInformation("Updating orders start");

        if (priorityId.HasValue && !await _orderPriorityRepository.Exists((int)priorityId, ct))
        {
            _logger.LogInformation("Priority {priorityId} not found", (int)priorityId);
            throw new NotFoundException($"Priority {priorityId} not found");
        }

        var Id = await _orderRepository.Update(id, priorityId, ct);

        _logger.LogInformation("Updating orders success");

        return Id;
    }

    public async Task<long> DeleteOrder(long id, CancellationToken ct)
    {
        _logger.LogInformation("Deleting orders start");

        var Id = await _orderRepository.Delete(id, ct);

        _logger.LogInformation("Deleting orders success");

        return Id;
    }

    public async Task<long> CloseOrder(long id, CancellationToken ct)
    {
        _logger.LogInformation("Closing orders start");

        if (!await _orderRepository.PossibleToComplete(id, ct))
        {
            _logger.LogInformation("Order{id} has unfinished works", id);
            throw new ConflictException("Order has unfinished works");
        }

        if (!await _orderRepository.PossibleToClose(id, ct))
        {
            _logger.LogInformation("Order{orderId} has not paid bill", id);
            throw new ConflictException($"Order{id} has not paid bill");
        }

        var Id = await _orderRepository.Close(id, ct);

        _logger.LogInformation("Closing orders success");

        return Id;
    }

    public async Task<long> CompleteOrder(long id, CancellationToken ct)
    {
        _logger.LogInformation("Closing orders start");

        if (!await _orderRepository.PossibleToComplete(id, ct))
        {
            _logger.LogInformation("Order{id} has unfinished works", id);
            throw new ConflictException("Order has unfinished works");
        }

        var Id = await _orderRepository.Complete(id, ct);

        _logger.LogInformation("Closing orders success");

        return Id;
    }
}
