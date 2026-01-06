using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.PartSet;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Repositories;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Services;

public class PartSetService : IPartSetService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IPositionRepository _positionRepository;
    private readonly IWorkProposalRepository _workProposalRepository;
    private readonly IPartSetRepository _partSetRepository;
    private readonly IBillRepository _billRepository;
    private readonly ILogger<PartSetService> _logger;

    public PartSetService(
        IOrderRepository orderRepository,
        IPositionRepository positionRepository,
        IWorkProposalRepository workProposalRepository,
        IPartSetRepository partSetRepository,
        IBillRepository billRepository,
        ILogger<PartSetService> logger)
    {
        _orderRepository = orderRepository;
        _positionRepository = positionRepository;
        _workProposalRepository = workProposalRepository;
        _partSetRepository = partSetRepository;
        _billRepository = billRepository;
        _logger = logger;
    }

    public async Task<List<PartSetItem>> GetPagedPartSets(PartSetFilter filter)
    {
        _logger.LogInformation("Getting part set start");

        var partSets = await _partSetRepository.GetPaged(filter);

        _logger.LogInformation("Getting part set success");

        return partSets;
    }

    public async Task<List<PartSetItem>> GetPartSetsByOrderId(long orderId)
    {
        _logger.LogInformation("Getting part set by order id start");

        var partSets = await _partSetRepository.GetByOrderId(orderId);

        _logger.LogInformation("Getting part set by order id success");

        return partSets;
    }

    public async Task<PartSetItem> GetPartSetById(long id)
    {
        _logger.LogInformation("Getting part set by id start");

        var partSet = await _partSetRepository.GetById(id);

        if (partSet is null)
        {
            _logger.LogError("Part set{partSetId} not found", id);
            throw new NotFoundException($"Part set {id} not found");
        }

        _logger.LogInformation("Getting part set by id success");

        return partSet;
    }

    public async Task<int> GetCountPartSets(PartSetFilter filter)
    {
        _logger.LogInformation("Getting count part set start");

        var count = await _partSetRepository.GetCount(filter);

        _logger.LogInformation("Getting count part set success");

        return count;
    }

    public async Task<long> AddToPartSet(PartSet partSet)
    {
        _logger.LogInformation("Adding to part set start");

        if (partSet.OrderId.HasValue && !await _orderRepository.Exists(partSet.OrderId.Value))
        {
            _logger.LogError("Order{OrderId} not found", partSet.OrderId);
            throw new NotFoundException($"Order{partSet.OrderId} not found");
        }

        if (partSet.ProposalId.HasValue && !await _workProposalRepository.Exists(partSet.ProposalId.Value))
        {
            _logger.LogError("Proposal{proposalId} not found", partSet.ProposalId);
            throw new NotFoundException($"Proposal{partSet.ProposalId} not found");
        }

        if (!await _positionRepository.Exists(partSet.PositionId))
        {
            _logger.LogError("Position{positionId} not found", partSet.PositionId);
            throw new NotFoundException($"Position {partSet.PositionId} not found");
        }

        var Id = await _partSetRepository.Create(partSet);

        _logger.LogInformation("Adding to part set success");

        if (partSet.OrderId.HasValue)
        {
            _logger.LogInformation("Recalculating bill start");
            await _billRepository.RecalculateAmount(partSet.OrderId.Value);
            _logger.LogInformation("Recalculating bill success");
        }

        return Id;
    }

    public async Task<long> UpdatePartSet(long id, PartSetUpdateModel model)
    {
        _logger.LogInformation("Updating part set start");

        var Id = await _partSetRepository.Update(id, model);

        var partSet = await _partSetRepository.GetById(id);

        if (partSet is not null && partSet.OrderId.HasValue)
        {
            _logger.LogInformation("Recalculating bill start");
            await _billRepository.RecalculateAmount(partSet.OrderId.Value);
            _logger.LogInformation("Recalculating bill success");
        }

        _logger.LogInformation("Updating part set success");

        return Id;
    }

    public async Task<long> DeleteFromPartSet(long id)
    {
        _logger.LogInformation("Deleting part set start");

        var Id = await _partSetRepository.Delete(id);

        var partSet = await _partSetRepository.GetById(id);

        if (partSet is not null && partSet.OrderId.HasValue)
        {
            _logger.LogInformation("Recalculating bill start");
            await _billRepository.RecalculateAmount(partSet.OrderId.Value);
            _logger.LogInformation("Recalculating bill success");
        }

        _logger.LogInformation("Deleting part set success");

        return Id;
    }
}
