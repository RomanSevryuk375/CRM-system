using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using CRMSystem.Core.ProjectionModels.WorkInOrder;
using Microsoft.Extensions.Logging;
using Shared.Enums;

namespace CRMSystem.Business.Services;

public class WorkInOrderService : IWorkInOrderService
{
    private readonly IWorkInOrderRepository _workInOrderRepository;
    private readonly IWorkInOrderStatusRepository _workInOrderStatusRepository;
    private readonly IWorkerRepository _workerRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IWorkRepository _workRepository;
    private readonly IBillRepository _billRepository;
    private readonly IUserContext _userContext;
    private readonly ILogger<WorkInOrderService> _logger;

    public WorkInOrderService(
        IWorkInOrderRepository workInOrderRepository,
        IWorkInOrderStatusRepository workInOrderStatusRepository,
        IWorkerRepository workerRepository,
        IOrderRepository orderRepository,
        IWorkRepository workRepository,
        IBillRepository billRepository,
        IUserContext userContext,
        ILogger<WorkInOrderService> logger)
    {
        _workInOrderRepository = workInOrderRepository;
        _workInOrderStatusRepository = workInOrderStatusRepository;
        _workerRepository = workerRepository;
        _orderRepository = orderRepository;
        _workRepository = workRepository;
        _billRepository = billRepository;
        _userContext = userContext;
        _logger = logger;
    }

    public async Task<List<WorkInOrderItem>> GetPagedWiO(WorkInOrderFilter filter, CancellationToken ct)
    {
        _logger.LogInformation("Getting paged works in order start");

        if (_userContext.RoleId != (int)RoleEnum.Manager)
            filter = filter with { WorkerIds = [(int)_userContext.ProfileId] };

        var works = await _workInOrderRepository.GetPaged(filter, ct);

        _logger.LogInformation("Getting paged works in order success");

        return works;
    }

    public async Task<int> GetCountWiO(WorkInOrderFilter filter, CancellationToken ct)
    {
        _logger.LogInformation("Getting count works in order start");

        var count = await _workInOrderRepository.GetCount(filter, ct);

        _logger.LogInformation("Getting count works in order success");

        return count;
    }

    public async Task<List<WorkInOrderItem>> GetWiOByOrderId(long orderId, CancellationToken ct)
    {
        _logger.LogInformation("Getting works in order by order id start");

        var works = await _workInOrderRepository.GetByOrderId(orderId, ct);

        _logger.LogInformation("Getting works in order by order id success");

        return works;
    }

    public async Task<long> CreateWiO(WorkInOrder workInOrder, CancellationToken ct)
    {
        _logger.LogInformation("Creating work in order start");

        if (!await _orderRepository.Exists(workInOrder.OrderId, ct))
        {
            _logger.LogError("Order{OrderId} not found", workInOrder.OrderId);
            throw new NotFoundException($"Order{workInOrder.OrderId} not found");
        }

        if (!await _workInOrderStatusRepository.Exists((int)workInOrder.StatusId, ct))
        {
            _logger.LogError("Status{StatusId} not found", workInOrder.StatusId);
            throw new NotFoundException($"Status{workInOrder.StatusId} not found");
        }

        if (!await _workerRepository.Exists(workInOrder.WorkerId, ct))
        {
            _logger.LogError("Worker {workerId} not found", workInOrder.WorkerId);
            throw new NotFoundException($"Worker {workInOrder.WorkerId} not found");
        }

        if (!await _workRepository.Exists(workInOrder.JobId, ct))
        {
            _logger.LogError("Work {workId} not found", workInOrder.JobId);
            throw new NotFoundException($"Work {workInOrder.JobId} not found");
        }

        var Id = await _workInOrderRepository.Create(workInOrder, ct);

        _logger.LogInformation("Creating work in order success");

        _logger.LogInformation("Recalculating bill start");
        await _billRepository.RecalculateAmount(workInOrder.OrderId, ct);
        _logger.LogInformation("Recalculating bill success");

        return Id;
    }

    public async Task<long> UpdateWiO(long id, WorkInOrderUpdateModel model, CancellationToken ct)
    {
        _logger.LogInformation("Updating work in order start");

        if (model.StatusId.HasValue && !await _workInOrderStatusRepository.Exists((int)model.StatusId.Value, ct))
        {
            _logger.LogError("Status{StatusId} not found", (int)model.StatusId);
            throw new NotFoundException($"Status{(int)model.StatusId} not found");
        }

        if (model.WorkerId.HasValue && !await _workerRepository.Exists(model.WorkerId.Value, ct))
        {
            _logger.LogError("Worker {workerId} not found", model.WorkerId);
            throw new NotFoundException($"Worker {model.WorkerId} not found");
        }

        var Id = await _workInOrderRepository.Update(id, model, ct);

        _logger.LogInformation("Updating work in order success");

        return Id;
    }

    public async Task<long> DeleteWIO(long id, CancellationToken ct)
    {
        _logger.LogInformation("Deleting work in order start");

        var Id = await _workInOrderRepository.Delete(id, ct);

        _logger.LogInformation("Deleting work in order success");

        return Id;
    }
}
