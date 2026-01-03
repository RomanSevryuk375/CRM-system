using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs.WorkInOrder;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Repositories;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Buisnes.Services;

public class WorkInOrderService : IWorkInOrderService
{
    private readonly IWorkInOrderRepository _workInOrderRepository;
    private readonly IWorkInOrderStatusRepository _workInOrderStatusRepository;
    private readonly IWorkerRepository _workerRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IWorkRepository _workRepository;
    private readonly ILogger<WorkInOrderService> _logger;

    public WorkInOrderService(
        IWorkInOrderRepository workInOrderRepository,
        IWorkInOrderStatusRepository workInOrderStatusRepository,
        IWorkerRepository workerRepository,
        IOrderRepository orderRepository,
        IWorkRepository workRepository,
        ILogger<WorkInOrderService> logger)
    {
        _workInOrderRepository = workInOrderRepository;
        _workInOrderStatusRepository = workInOrderStatusRepository;
        _workerRepository = workerRepository;
        _orderRepository = orderRepository;
        _workRepository = workRepository;
        _logger = logger;
    }

    public async Task<List<WorkInOrderItem>> GetPagedWiO(WorkInOrderFilter filter)
    {
        _logger.LogInformation("Getting paged works in order start");

        var works = await _workInOrderRepository.GetPaged(filter);

        _logger.LogInformation("Getting paged works in order success");

        return works;
    }

    public async Task<int> GetCountWiO(WorkInOrderFilter filter)
    {
        _logger.LogInformation("Getting count works in order start");

        var count = await _workInOrderRepository.GetCount(filter);

        _logger.LogInformation("Getting count works in order success");

        return count;
    }

    public async Task<List<WorkInOrderItem>> GetWiOByOrderId(long orderId)
    {
        _logger.LogInformation("Getting works in order by order id start");

        var works = await _workInOrderRepository.GetByOrderId(orderId);

        _logger.LogInformation("Getting works in order by order id success");

        return works;
    }

    public async Task<long> CreateWiO(WorkInOrder workInOrder)
    {
        _logger.LogInformation("Creating work in order start");

        if (!await _orderRepository.Exists(workInOrder.OrderId))
        {
            _logger.LogError("Order{OrderId} not found", workInOrder.OrderId);
            throw new NotFoundException($"Order{workInOrder.OrderId} not found");
        }

        if (!await _workInOrderStatusRepository.Exists((int)workInOrder.StatusId))
        {
            _logger.LogError("Status{StatusId} not found", workInOrder.StatusId);
            throw new NotFoundException($"Status{workInOrder.StatusId} not found");
        }

        if (!await _workerRepository.Exists(workInOrder.WorkerId))
        {
            _logger.LogError("Worker {workerId} not found", workInOrder.WorkerId);
            throw new NotFoundException($"Worker {workInOrder.WorkerId} not found");
        }

        if (!await _workRepository.Exists(workInOrder.JobId))
        {
            _logger.LogError("Work {workId} not found", workInOrder.JobId);
            throw new NotFoundException($"Work {workInOrder.JobId} not found");
        }

        var Id = await _workInOrderRepository.Create(workInOrder);

        _logger.LogInformation("Creating work in order success");

        return Id;
    }

    public async Task<long> UpdateWiO(long id, WorkInOrderUpdateModel model)
    {
        _logger.LogInformation("Updating work in order start");

        if (model.StatusId.HasValue && !await _workInOrderStatusRepository.Exists((int)model.StatusId.Value))
        {
            _logger.LogError("Status{StatusId} not found", (int)model.StatusId);
            throw new NotFoundException($"Status{(int)model.StatusId} not found");
        }

        if (model.WorkerId.HasValue && !await _workerRepository.Exists(model.WorkerId.Value))
        {
            _logger.LogError("Worker {workerId} not found", model.WorkerId);
            throw new NotFoundException($"Worker {model.WorkerId} not found");
        }

        var Id = await _workInOrderRepository.Update(id, model);

        _logger.LogInformation("Updating work in order success");

        return Id;
    }

    public async Task<long> DeleteWIO(long id)
    {
        _logger.LogInformation("Deleting work in order start");

        var Id = await _workInOrderRepository.Delete(id);

        _logger.LogInformation("Deleting work in order success");

        return Id;
    }
}
