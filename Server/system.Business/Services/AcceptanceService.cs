using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.Acceptance;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using Microsoft.Extensions.Logging;
using Shared.Enums;
using Shared.Filters;

namespace CRMSystem.Business.Services;

public class AcceptanceService(
    IAcceptanceRepository acceptanceRepository,
    IOrderRepository orderRepository,
    IWorkerRepository workerRepository,
    ILogger<AcceptanceImgService> logger) : IAcceptanceService
{
    public async Task<List<AcceptanceItem>> GetPagedAcceptance(AcceptanceFilter filter, CancellationToken ct)
    {
        logger.LogInformation("Getting acceptance start");

        var attachment = await acceptanceRepository.GetPaged(filter, ct);

        logger.LogInformation("Getting acceptance success");

        return attachment;
    }

    public async Task<int> GetCountAcceptance(AcceptanceFilter filter, CancellationToken ct)
    {
        logger.LogInformation("Getting count acceptance start");

        var count = await acceptanceRepository.GetCount(filter, ct);

        logger.LogInformation("Getting count acceptance success");
        return count;
    }

    public async Task<long> CreateAcceptance(Acceptance acceptance, CancellationToken ct)
    {
        logger.LogInformation("Creating acceptance start");

        if(!await workerRepository.Exists(acceptance.WorkerId, ct))
        { 
            logger.LogInformation("Worker{WorkerId} not found", acceptance.WorkerId);
            throw new NotFoundException($"Worker {acceptance.WorkerId} not found");
        }

        if (!await orderRepository.Exists(acceptance.OrderId, ct))
        {
            logger.LogInformation("Order{OrderId} not found", acceptance.OrderId);
            throw new NotFoundException($"Order {acceptance.OrderId} not found");
        }

        if (await orderRepository.GetStatus(acceptance.OrderId, ct) == (int)OrderStatusEnum.Completed ||
            await orderRepository.GetStatus(acceptance.OrderId, ct) == (int)OrderStatusEnum.Closed)
        {
            logger.LogInformation("Order{OrderId} is completed or closed", acceptance.OrderId);
            throw new ConflictException($"Order {acceptance.OrderId} is completed or closed");
        }

        logger.LogInformation("Creating acceptance success");

        return await acceptanceRepository.Create(acceptance, ct);
    }

    public async Task<long> UpdateAcceptance(long id, AcceptanceUpdateModel model, CancellationToken ct)
    {
        logger.LogInformation("Getting acceptance start");

        var attachment = await acceptanceRepository.Update(id, model, ct);

        logger.LogInformation("Getting acceptance success");

        return attachment;
    }

    public async Task<long> DeleteAcceptance(long id, CancellationToken ct)
    {
        logger.LogInformation("Getting acceptance start");

        var attachment = await acceptanceRepository.Delete(id, ct);

        logger.LogInformation("Getting acceptance success");

        return attachment;
    }
}
