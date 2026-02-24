using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using CRMSystem.Core.ProjectionModels.WorkInOrder;
using Microsoft.Extensions.Logging;
using Shared.Enums;
using Shared.Filters;

namespace CRMSystem.Business.Services;

public class WorkInOrderService(
    IWorkInOrderRepository workInOrderRepository,
    IWorkInOrderStatusRepository workInOrderStatusRepository,
    IWorkerRepository workerRepository,
    IOrderRepository orderRepository,
    IWorkRepository workRepository,
    IBillRepository billRepository,
    IUserContext userContext,
    ILogger<WorkInOrderService> logger) : IWorkInOrderService
{
    public async Task<List<WorkInOrderItem>> GetPagedWiO(WorkInOrderFilter filter, CancellationToken ct)
    {
        logger.LogInformation("Getting paged works in order start");

        if (userContext.RoleId != (int)RoleEnum.Manager)
            filter = filter with { WorkerIds = [(int)userContext.ProfileId] };

        var works = await workInOrderRepository.GetPaged(filter, ct);

        logger.LogInformation("Getting paged works in order success");

        return works;
    }

    public async Task<int> GetCountWiO(WorkInOrderFilter filter, CancellationToken ct)
    {
        logger.LogInformation("Getting count works in order start");

        var count = await workInOrderRepository.GetCount(filter, ct);

        logger.LogInformation("Getting count works in order success");

        return count;
    }

    public async Task<List<WorkInOrderItem>> GetWiOByOrderId(long orderId, CancellationToken ct)
    {
        logger.LogInformation("Getting works in order by order id start");

        var works = await workInOrderRepository.GetByOrderId(orderId, ct);

        logger.LogInformation("Getting works in order by order id success");

        return works;
    }

    public async Task<long> CreateWiO(WorkInOrderCreateModel createModel, CancellationToken ct)
    {
        logger.LogInformation("Creating work in order start");

        if (!await orderRepository.Exists(createModel.OrderId, ct))
        {
            logger.LogError("Order{OrderId} not found", createModel.OrderId);
            throw new NotFoundException($"Order{createModel.OrderId} not found");
        }

        if (!await workInOrderStatusRepository.Exists((int)createModel.StatusId, ct))
        {
            logger.LogError("Status{StatusId} not found", createModel.StatusId);
            throw new NotFoundException($"Status{createModel.StatusId} not found");
        }

        if (!await workerRepository.Exists(createModel.WorkerId, ct))
        {
            logger.LogError("Worker {workerId} not found", createModel.WorkerId);
            throw new NotFoundException($"Worker {createModel.WorkerId} not found");
        }

        if (!await workRepository.Exists(createModel.JobId, ct))
        {
            logger.LogError("Work {workId} not found", createModel.JobId);
            throw new NotFoundException($"Work {createModel.JobId} not found");
        }
        
        var (work, errors) = WorkInOrder.Create(
            0,
            createModel.OrderId,
            createModel.JobId,
            createModel.WorkerId,
            createModel.StatusId,
            createModel.TimeSpent);

        if (errors is not null && errors.Any())
        {
            throw new ValidationException(string.Join(", ", errors));
        }

        var id = await workInOrderRepository.Create(work!, ct);

        logger.LogInformation("Creating work in order success");

        logger.LogInformation("Recalculating bill start");
        await billRepository.RecalculateAmount(work!.OrderId, ct);
        logger.LogInformation("Recalculating bill success");

        return id;
    }

    public async Task<long> UpdateWiO(long id, WorkInOrderUpdateModel model, CancellationToken ct)
    {
        logger.LogInformation("Updating work in order start");

        if (model.StatusId.HasValue && !await workInOrderStatusRepository.Exists((int)model.StatusId.Value, ct))
        {
            logger.LogError("Status{StatusId} not found", (int)model.StatusId);
            throw new NotFoundException($"Status{(int)model.StatusId} not found");
        }

        if (model.WorkerId.HasValue && !await workerRepository.Exists(model.WorkerId.Value, ct))
        {
            logger.LogError("Worker {workerId} not found", model.WorkerId);
            throw new NotFoundException($"Worker {model.WorkerId} not found");
        }

        var workId = await workInOrderRepository.Update(id, model, ct);

        logger.LogInformation("Updating work in order success");

        return workId;
    }

    public async Task<long> DeleteWio(long id, CancellationToken ct)
    {
        logger.LogInformation("Deleting work in order start");

        var workId = await workInOrderRepository.Delete(id, ct);

        logger.LogInformation("Deleting work in order success");

        return workId;
    }
}
