using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.Order;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using Microsoft.Extensions.Logging;
using Shared.Enums;
using Shared.Filters;

namespace CRMSystem.Business.Services;

public class OrderService(
    IOrderRepository orderRepository,
    IOrderStatusRepository orderStatusRepository,
    ICarRepository carRepository,
    IOrderPriorityRepository orderPriorityRepository,
    IBillRepository billRepository,
    IUserContext userContext,
    ILogger<OrderService> logger,
    IUnitOfWork unitOfWork) : IOrderService
{
    public async Task<List<OrderItem>> GetPagedOrders(OrderFilter filter, CancellationToken ct)
    {
        logger.LogInformation("Getting orders start");

        filter = userContext.RoleId switch
        {
            (int)RoleEnum.Worker => filter with { WorkerIds = [(int)userContext.ProfileId] },
            (int)RoleEnum.Client => filter with { ClientIds = [userContext.ProfileId] },
            _ => filter
        };

        var orders = await orderRepository.GetPaged(filter, ct);

        logger.LogInformation("Getting orders success");

        return orders;
    }

    public async Task<int> GetCountOrders(OrderFilter filter, CancellationToken ct)
    {
        logger.LogInformation("Getting count orders start");

        var count = await orderRepository.GetCount(filter, ct);

        logger.LogInformation("Getting count orders success");

        return count;
    }

    public async Task<long> CreateOrder(Order order, CancellationToken ct)
    {
        logger.LogInformation("Creating orders start");

        if (!await carRepository.Exists(order.CarId, ct))
        {
            logger.LogError("Car {CarId} not found", order.CarId);
            throw new NotFoundException($"Car {order.CarId} not found");
        }

        if (!await orderPriorityRepository.Exists((int)order.PriorityId, ct))
        {
            logger.LogInformation("Priority {priorityId} not found", (int)order.PriorityId);
            throw new NotFoundException($"Priority {order.PriorityId} not found");
        }

        if (!await orderStatusRepository.Exists((int)order.StatusId, ct))
        {
            logger.LogInformation("Status{statusId} not found", order.StatusId);
            throw new NotFoundException($"Status {order.StatusId} not found");
        }

        var Id = await orderRepository.Create(order, ct);

        logger.LogInformation("Creating orders success");

        return Id;
    }

    public async Task<long> CreateOrderWithBill(Order order, Bill bill, CancellationToken ct)
    {
        await unitOfWork.BeginTransactionAsync(ct);

        try
        {
            logger.LogInformation("Creating orders start");

            if (!await carRepository.Exists(order.CarId, ct))
            {
                logger.LogError("Car {CarId} not found", order.CarId);
                throw new NotFoundException($"Car {order.CarId} not found");
            }

            if (!await orderPriorityRepository.Exists((int)order.PriorityId, ct))
            {
                logger.LogInformation("Priority {priorityId} not found", (int)order.PriorityId);
                throw new NotFoundException($"Priority {order.PriorityId} not found");
            }

            if (!await orderStatusRepository.Exists((int)order.StatusId, ct))
            {
                logger.LogInformation("Status{statusId} not found", order.StatusId);
                throw new NotFoundException($"Status {order.StatusId} not found");
            }

            var orderId = await orderRepository.Create(order, ct);

            bill.SetOrderId(orderId);
            await billRepository.Create(bill, ct);

            logger.LogInformation("Creating orders success");

            await unitOfWork.CommitTransactionAsync(ct);

            return orderId;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Transaction failed. Rolling back all changes.");
            
            await unitOfWork.RollbackAsync(ct);

            throw;
        }
    }

    public async Task<long> UpdateOrder(long id, OrderPriorityEnum? priorityId, CancellationToken ct)
    {
        logger.LogInformation("Updating orders start");

        if (priorityId.HasValue && !await orderPriorityRepository.Exists((int)priorityId, ct))
        {
            logger.LogInformation("Priority {priorityId} not found", (int)priorityId);
            throw new NotFoundException($"Priority {priorityId} not found");
        }

        var Id = await orderRepository.Update(id, priorityId, ct);

        logger.LogInformation("Updating orders success");

        return Id;
    }

    public async Task<long> DeleteOrder(long id, CancellationToken ct)
    {
        logger.LogInformation("Deleting orders start");

        var Id = await orderRepository.Delete(id, ct);

        logger.LogInformation("Deleting orders success");

        return Id;
    }

    public async Task<long> CloseOrder(long id, CancellationToken ct)
    {
        logger.LogInformation("Closing orders start");

        if (!await orderRepository.PossibleToComplete(id, ct))
        {
            logger.LogInformation("Order{id} has unfinished works", id);
            throw new ConflictException("Order has unfinished works");
        }

        if (!await orderRepository.PossibleToClose(id, ct))
        {
            logger.LogInformation("Order{orderId} has not paid bill", id);
            throw new ConflictException($"Order{id} has not paid bill");
        }

        var Id = await orderRepository.Close(id, ct);

        logger.LogInformation("Closing orders success");

        return Id;
    }

    public async Task<long> CompleteOrder(long id, CancellationToken ct)
    {
        logger.LogInformation("Closing orders start");

        if (!await orderRepository.PossibleToComplete(id, ct))
        {
            logger.LogInformation("Order{id} has unfinished works", id);
            throw new ConflictException("Order has unfinished works");
        }

        var Id = await orderRepository.Complete(id, ct);

        logger.LogInformation("Closing orders success");

        return Id;
    }
}
