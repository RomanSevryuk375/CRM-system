using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs.Acceptance;
using CRMSystem.Core.Enums;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Repositories;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Buisnes.Services;

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

    public async Task<List<AcceptanceItem>> GetPagedAcceptance(AcceptanceFilter filter)
    {
        _logger.LogInformation("Getting acceptance start");

        var attachment = await _acceptanceRepository.GetPaged(filter);

        _logger.LogInformation("Getting acceptance success");

        return attachment;
    }

    public async Task<int> GetCountAcceptance(AcceptanceFilter filter)
    {
        _logger.LogInformation("Getting count acceptance start");

        var count = await _acceptanceRepository.GetCount(filter);

        _logger.LogInformation("Getting count acceptance success");
        return count;
    }

    public async Task<long> CreateAcceptance(Acceptance acceptance)
    {
        _logger.LogInformation("Creating acceptance start");

        if(!await _workerRepository.Exists(acceptance.WorkerId))
        { 
            _logger.LogInformation("Worker{WorkerId} not found", acceptance.WorkerId);
            throw new NotFoundException($"Worker {acceptance.WorkerId} not found");
        }

        if (!await _orderRepository.Exists(acceptance.OrderId))
        {
            _logger.LogInformation("Order{OrderId} not found", acceptance.OrderId);
            throw new NotFoundException($"Order {acceptance.OrderId} not found");
        }

        if (await _orderRepository.GetStatus(acceptance.OrderId) == (int)OrderStatusEnum.Completed ||
            await _orderRepository.GetStatus(acceptance.OrderId) == (int)OrderStatusEnum.Closed)
        {
            _logger.LogInformation("Order{OrderId} is completed or closed", acceptance.OrderId);
            throw new ConflictException($"Order {acceptance.OrderId} is completed or closed");
        }

        _logger.LogInformation("Creating acceptance success");

        return await _acceptanceRepository.Create(acceptance);
    }

    public async Task<long> UpdateAcceptance(long id, AcceptanceUpdateModel model)
    {
        _logger.LogInformation("Getting acceptance start");

        var attachment = await _acceptanceRepository.Update(id, model);

        _logger.LogInformation("Getting acceptance success");

        return attachment;
    }

    public async Task<long> DeleteAcceptance(long id)
    {
        _logger.LogInformation("Getting acceptance start");

        var attachment = await _acceptanceRepository.Delete(id);

        _logger.LogInformation("Getting acceptance success");

        return attachment;
    }
}
