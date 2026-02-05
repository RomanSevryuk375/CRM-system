using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.PartSet;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using Microsoft.Extensions.Logging;
using Shared.Filters;

namespace CRMSystem.Business.Services;

public class PartSetService(
    IOrderRepository orderRepository,
    IPositionRepository positionRepository,
    IWorkProposalRepository workProposalRepository,
    IPartSetRepository partSetRepository,
    IBillRepository billRepository,
    ILogger<PartSetService> logger) : IPartSetService
{
    public async Task<List<PartSetItem>> GetPagedPartSets(PartSetFilter filter, CancellationToken ct)
    {
        logger.LogInformation("Getting part set start");

        var partSets = await partSetRepository.GetPaged(filter, ct);

        logger.LogInformation("Getting part set success");

        return partSets;
    }

    public async Task<List<PartSetItem>> GetPartSetsByOrderId(long orderId, CancellationToken ct)
    {
        logger.LogInformation("Getting part set by order id start");

        var partSets = await partSetRepository.GetByOrderId(orderId, ct);

        logger.LogInformation("Getting part set by order id success");

        return partSets;
    }

    public async Task<PartSetItem> GetPartSetById(long id, CancellationToken ct)
    {
        logger.LogInformation("Getting part set by id start");

        var partSet = await partSetRepository.GetById(id, ct);

        if (partSet is null)
        {
            logger.LogError("Part set{partSetId} not found", id);
            throw new NotFoundException($"Part set {id} not found");
        }

        logger.LogInformation("Getting part set by id success");

        return partSet;
    }

    public async Task<int> GetCountPartSets(PartSetFilter filter, CancellationToken ct)
    {
        logger.LogInformation("Getting count part set start");

        var count = await partSetRepository.GetCount(filter, ct);

        logger.LogInformation("Getting count part set success");

        return count;
    }

    public async Task<long> AddToPartSet(PartSet partSet, CancellationToken ct)
    {
        logger.LogInformation("Adding to part set start");

        if (partSet.OrderId.HasValue && !await orderRepository.Exists(partSet.OrderId.Value, ct))
        {
            logger.LogError("Order{OrderId} not found", partSet.OrderId);
            throw new NotFoundException($"Order{partSet.OrderId} not found");
        }

        if (partSet.ProposalId.HasValue && !await workProposalRepository.Exists(partSet.ProposalId.Value, ct))
        {
            logger.LogError("Proposal{proposalId} not found", partSet.ProposalId);
            throw new NotFoundException($"Proposal{partSet.ProposalId} not found");
        }

        if (!await positionRepository.Exists(partSet.PositionId, ct))
        {
            logger.LogError("Position{positionId} not found", partSet.PositionId);
            throw new NotFoundException($"Position {partSet.PositionId} not found");
        }

        var Id = await partSetRepository.Create(partSet, ct);

        logger.LogInformation("Adding to part set success");

        if (partSet.OrderId.HasValue)
        {
            logger.LogInformation("Recalculating bill start");
            await billRepository.RecalculateAmount(partSet.OrderId.Value, ct);
            logger.LogInformation("Recalculating bill success");
        }

        return Id;
    }

    public async Task<long> UpdatePartSet(long id, PartSetUpdateModel model, CancellationToken ct)
    {
        logger.LogInformation("Updating part set start");

        var Id = await partSetRepository.Update(id, model, ct);

        var partSet = await partSetRepository.GetById(id, ct);

        if (partSet is not null && partSet.OrderId.HasValue)
        {
            logger.LogInformation("Recalculating bill start");
            await billRepository.RecalculateAmount(partSet.OrderId.Value, ct);
            logger.LogInformation("Recalculating bill success");
        }

        logger.LogInformation("Updating part set success");

        return Id;
    }

    public async Task<long> DeleteFromPartSet(long id, CancellationToken ct)
    {
        logger.LogInformation("Deleting part set start");

        var Id = await partSetRepository.Delete(id, ct);

        var partSet = await partSetRepository.GetById(id, ct);

        if (partSet is not null && partSet.OrderId.HasValue)
        {
            logger.LogInformation("Recalculating bill start");
            await billRepository.RecalculateAmount(partSet.OrderId.Value, ct);
            logger.LogInformation("Recalculating bill success");
        }

        logger.LogInformation("Deleting part set success");

        return Id;
    }
}
