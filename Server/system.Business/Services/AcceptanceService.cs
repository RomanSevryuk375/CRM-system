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

    public async Task<long> CreateAcceptance(AcceptanceCreateModel createModel, CancellationToken ct)
    {
        logger.LogInformation("Creating acceptance start");

        if(!await workerRepository.Exists(createModel.WorkerId, ct))
        { 
            logger.LogInformation("Worker{WorkerId} not found", createModel.WorkerId);
            throw new NotFoundException($"Worker {createModel.WorkerId} not found");
        }

        if (!await orderRepository.Exists(createModel.OrderId, ct))
        {
            logger.LogInformation("Order{OrderId} not found", createModel.OrderId);
            throw new NotFoundException($"Order {createModel.OrderId} not found");
        }

        if (await orderRepository.GetStatus(createModel.OrderId, ct) == (int)OrderStatusEnum.Completed ||
            await orderRepository.GetStatus(createModel.OrderId, ct) == (int)OrderStatusEnum.Closed)
        {
            logger.LogInformation("Order{OrderId} is completed or closed", createModel.OrderId);
            throw new ConflictException($"Order {createModel.OrderId} is completed or closed");
        }
        
        var (acceptance, errors) = Acceptance.Create(
            0,
            createModel.OrderId,
            createModel.WorkerId,
            createModel.CreatedAt,
            createModel.Mileage,
            createModel.FuelLevel,
            createModel.ExternalDefects,
            createModel.InternalDefects,
            createModel.ClientSign,
            createModel.WorkerSign);

        if (errors is not null && errors.Any())
        {
            throw new ValidationException(string.Join(", ", errors));
        }

        logger.LogInformation("Creating acceptance success");

        return await acceptanceRepository.Create(acceptance!, ct);
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
