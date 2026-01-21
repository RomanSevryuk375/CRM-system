using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.Acceptance;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using Microsoft.Extensions.Logging;
using Shared.Enums;
using Shared.Filters;

namespace CRMSystem.Business.Services;

public class AcceptanceService : IAcceptanceService
{
    private readonly IAcceptanceRepository _acceptanceRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IWorkerRepository _workerRepository;
    private readonly ILogger<AcceptanceImgService> _logger;

    public AcceptanceService(
        IAcceptanceRepository acceptanceRepository,
        IOrderRepository orderRepository,
        IWorkerRepository workerRepository,
        ILogger<AcceptanceImgService> logger)
    {
        _acceptanceRepository = acceptanceRepository;
        _orderRepository = orderRepository;
        _workerRepository = workerRepository;
        _logger = logger;
    }

    public async Task<List<AcceptanceItem>> GetPagedAcceptance(AcceptanceFilter filter, CancellationToken ct)
    {
        _logger.LogInformation("Getting acceptance start");

        var attachment = await _acceptanceRepository.GetPaged(filter, ct);

        _logger.LogInformation("Getting acceptance success");

        return attachment;
    }

    public async Task<int> GetCountAcceptance(AcceptanceFilter filter, CancellationToken ct)
    {
        _logger.LogInformation("Getting count acceptance start");

        var count = await _acceptanceRepository.GetCount(filter, ct);

        _logger.LogInformation("Getting count acceptance success");
        return count;
    }

    public async Task<long> CreateAcceptance(Acceptance acceptance, CancellationToken ct)
    {
        _logger.LogInformation("Creating acceptance start");

        if(!await _workerRepository.Exists(acceptance.WorkerId, ct))
        { 
            _logger.LogInformation("Worker{WorkerId} not found", acceptance.WorkerId);
            throw new NotFoundException($"Worker {acceptance.WorkerId} not found");
        }

        if (!await _orderRepository.Exists(acceptance.OrderId, ct))
        {
            _logger.LogInformation("Order{OrderId} not found", acceptance.OrderId);
            throw new NotFoundException($"Order {acceptance.OrderId} not found");
        }

        if (await _orderRepository.GetStatus(acceptance.OrderId, ct) == (int)OrderStatusEnum.Completed ||
            await _orderRepository.GetStatus(acceptance.OrderId, ct) == (int)OrderStatusEnum.Closed)
        {
            _logger.LogInformation("Order{OrderId} is completed or closed", acceptance.OrderId);
            throw new ConflictException($"Order {acceptance.OrderId} is completed or closed");
        }

        _logger.LogInformation("Creating acceptance success");

        return await _acceptanceRepository.Create(acceptance, ct);
    }

    public async Task<long> UpdateAcceptance(long id, AcceptanceUpdateModel model, CancellationToken ct)
    {
        _logger.LogInformation("Getting acceptance start");

        var attachment = await _acceptanceRepository.Update(id, model, ct);

        _logger.LogInformation("Getting acceptance success");

        return attachment;
    }

    public async Task<long> DeleteAcceptance(long id, CancellationToken ct)
    {
        _logger.LogInformation("Getting acceptance start");

        var attachment = await _acceptanceRepository.Delete(id, ct);

        _logger.LogInformation("Getting acceptance success");

        return attachment;
    }
}
