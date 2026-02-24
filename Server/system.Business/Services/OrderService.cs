using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.Order;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using CRMSystem.Core.ProjectionModels.Bill;
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
    IOrderPdfService pdfService,
    IFileService fileService,
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

    public async Task<long> CreateOrder(OrderCreateModel createModel, CancellationToken ct)
    {
        logger.LogInformation("Creating orders start");

        if (!await carRepository.Exists(createModel.CarId, ct))
        {
            logger.LogError("Car {CarId} not found", createModel.CarId);
            throw new NotFoundException($"Car {createModel.CarId} not found");
        }

        if (!await orderPriorityRepository.Exists((int)createModel.PriorityId, ct))
        {
            logger.LogInformation("Priority {priorityId} not found", (int)createModel.PriorityId);
            throw new NotFoundException($"Priority {createModel.PriorityId} not found");
        }

        if (!await orderStatusRepository.Exists((int)createModel.StatusId, ct))
        {
            logger.LogInformation("Status{statusId} not found", createModel.StatusId);
            throw new NotFoundException($"Status {createModel.StatusId} not found");
        }
        
        var (order, errors) = Order.Create(
            0,
            createModel.StatusId,
            createModel.CarId,
            createModel.Date,
            createModel.PriorityId);

        if (errors is not null && errors.Any())
        {
            throw new ValidationException(string.Join(", ", errors));
        }

        var id = await orderRepository.Create(order!, ct);

        logger.LogInformation("Creating orders success");

        return id;
    }

    public async Task<long> CreateOrderWithBill(
        OrderCreateModel orderCreateModel, 
        BillCreateModel billCreateModel,
        CancellationToken ct)
    {
        await unitOfWork.BeginTransactionAsync(ct);

        try
        {
            logger.LogInformation("Creating orders start");

            if (!await carRepository.Exists(orderCreateModel.CarId, ct))
            {
                logger.LogError("Car {CarId} not found", orderCreateModel.CarId);
                throw new NotFoundException($"Car {orderCreateModel.CarId} not found");
            }

            if (!await orderPriorityRepository.Exists((int)orderCreateModel.PriorityId, ct))
            {
                logger.LogInformation("Priority {priorityId} not found", (int)orderCreateModel.PriorityId);
                throw new NotFoundException($"Priority {orderCreateModel.PriorityId} not found");
            }

            if (!await orderStatusRepository.Exists((int)orderCreateModel.StatusId, ct))
            {
                logger.LogInformation("Status{statusId} not found", orderCreateModel.StatusId);
                throw new NotFoundException($"Status {orderCreateModel.StatusId} not found");
            }
            
            var (order, errorsOrder) = Order.Create(
                0,
                orderCreateModel.StatusId,
                orderCreateModel.CarId,
                orderCreateModel.Date,
                orderCreateModel.PriorityId);

            if (errorsOrder is not null && errorsOrder.Any())
            {
                throw new ValidationException(string.Join(", ", errorsOrder));
            }

            var orderId = await orderRepository.Create(order!, ct);
            
            var (bill, errorsBill) = Bill.Create(
                0,
                orderId,
                billCreateModel.StatusId,
                billCreateModel.CreatedAt,
                billCreateModel.Amount,
                billCreateModel.ActualBillDate);

            if (errorsBill is not null && errorsBill.Any())
            {
                throw new ValidationException(string.Join(", ", errorsBill));
            }
            await billRepository.Create(bill!, ct);

            logger.LogInformation("Creating order success");

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

        var orderId = await orderRepository.Update(id, priorityId, ct);

        logger.LogInformation("Updating orders success");

        return orderId;
    }

    public async Task<long> DeleteOrder(long id, CancellationToken ct)
    {
        logger.LogInformation("Deleting orders start");

        var orderId = await orderRepository.Delete(id, ct);

        logger.LogInformation("Deleting orders success");

        return orderId;
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

        var orderId = await orderRepository.Close(id, ct);

        logger.LogInformation("Closing orders success");

        return orderId;
    }

    public async Task<long> CompleteOrder(long id, CancellationToken ct)
    {
        logger.LogInformation("Closing orders start");

        if (!await orderRepository.PossibleToComplete(id, ct))
        {
            logger.LogInformation("Order{id} has unfinished works", id);
            throw new ConflictException("Order has unfinished works");
        }

        var orderId = await orderRepository.Complete(id, ct);

        logger.LogInformation("Closing orders success");

        return orderId;
    }
    
    public async Task<string> CreateOrderPdfAndUpload(long orderId, CancellationToken ct)
    {
        var orderData = await orderRepository.GetDocumentData(orderId, ct)
                        ?? throw new NullReferenceException("data is null"); 
        
        var pdfBytes = pdfService.GenerateOrderPdf(orderData);

        using var stream = new MemoryStream(pdfBytes);
        var fileName = $"order_{orderId}_{DateTime.Now:yyyyMMdd}.pdf";
    
        var filePath = await fileService.UploadFile(stream, fileName, "application/pdf", ct);

        return filePath; 
    }
}
