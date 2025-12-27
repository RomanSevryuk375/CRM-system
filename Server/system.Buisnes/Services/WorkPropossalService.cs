using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs.WorkProposal;
using CRMSystem.Core.Enums;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Repositories;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Buisnes.Services;

public class WorkPropossalService : IWorkPropossalService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IWorkRepository _workRepository;
    private readonly IWorkerRepository _workerRepository;
    private readonly IWorkProposalRepository _workProposalRepository;
    private readonly IWorkProposalStatusRepository _workProposalStatusRepository;
    private readonly IWorkInOrderRepository _workInOrderRepository;
    private readonly IPartSetRepository _partSetRepository;
    private readonly IBillRepository _billRepository;
    private readonly ILogger<WorkPropossalService> _logger;

    public WorkPropossalService(
        IOrderRepository orderRepository,
        IWorkRepository workRepository,
        IWorkerRepository workerRepository,
        IWorkProposalRepository workProposalRepository,
        IWorkProposalStatusRepository workProposalStatusRepository,
        IWorkInOrderRepository workInOrderRepository,
        IPartSetRepository partSetRepository,
        IBillRepository billRepository,
        ILogger<WorkPropossalService> logger)
    {
        _orderRepository = orderRepository;
        _workRepository = workRepository;
        _workerRepository = workerRepository;
        _workProposalRepository = workProposalRepository;
        _workProposalStatusRepository = workProposalStatusRepository;
        _workInOrderRepository = workInOrderRepository;
        _partSetRepository = partSetRepository;
        _billRepository = billRepository;
        _logger = logger;
    }

    public async Task<List<WorkProposalItem>> GetPagedProposals(WorkProposalFilter filter)
    {
        _logger.LogInformation("Getting proposals start");

        var proposals = await _workProposalRepository.Getpaged(filter);

        _logger.LogInformation("Getting proposals success");

        return proposals;
    }

    public async Task<int> GetCountProposals(WorkProposalFilter filter)
    {
        _logger.LogInformation("Getting count proposals start");

        var count = await _workProposalRepository.GetCount(filter);

        _logger.LogInformation("Getting count proposals success");

        return count;
    }

    public async Task<WorkProposalItem> GetProposalById(long id)
    {
        _logger.LogInformation("Getting proposal by id start");

        var proposal = await _workProposalRepository.GetById(id);

        if (proposal is null)
        {
            _logger.LogError("Proposal{proposalId} not found", id);
            throw new NotFoundException($"Proposal{id} not found");
        }

        _logger.LogInformation("Getting proposal by id success");

        return proposal;
    }

    public async Task<long> CreateProposal(WorkProposal workProposal)
    {
        _logger.LogInformation("Creating proposal start");

        if (!await _orderRepository.Exists(workProposal.OrderId))
        {
            _logger.LogError("Order{OrderId} not found", workProposal.OrderId);
            throw new NotFoundException($"Order{workProposal.OrderId} not found");
        }

        if (!await _workProposalStatusRepository.Exists((int)workProposal.StatusId))
        {
            _logger.LogError("Status{StatusId} not found", workProposal.StatusId);
            throw new NotFoundException($"Status{workProposal.StatusId} not found");
        }

        if (!await _workerRepository.Exists(workProposal.WorkerId))
        {
            _logger.LogError("Worker {workerId} not found", workProposal.WorkerId);
            throw new NotFoundException($"Worker {workProposal.WorkerId} not found");
        }

        if (!await _workRepository.Exists(workProposal.JobId))
        {
            _logger.LogError("Work {workId} not found", workProposal.JobId);
            throw new NotFoundException($"Work {workProposal.JobId} not found");
        }

        var Id = await _workProposalRepository.Create(workProposal);

        _logger.LogInformation("Creating proposal success");

        return Id;
    }

    public async Task<long> UpdateProposal(long id, ProposalStatusEnum? statusId)
    {
        _logger.LogInformation("Updating proposal start");

        if (statusId.HasValue && !await _workProposalStatusRepository.Exists((int)statusId.Value))
        {
            _logger.LogError("Status{StatusId} not found", statusId);
            throw new NotFoundException($"Status{statusId} not found");
        }

        var Id = await _workProposalRepository.Update(id, statusId);

        _logger.LogInformation("Updating proposal success");

        return Id;
    }

    public async Task<long> DeleteProposal(long id)
    {
        _logger.LogInformation("Deleting proposal start");

        var Id = await _workProposalRepository.Delete(id);

        _logger.LogInformation("Deleting proposal success");

        return Id;
    }

    public async Task<long> AcceptProposal(long id)
    {
        _logger.LogInformation("Accepting proposal start");

        var proposal = await _workProposalRepository.GetById(id);
        _logger.LogInformation("Getting proposal by id success");

        var Id = await _workProposalRepository.AcceptProposal(id);

        var transitModel = new WorkInOrder(
            0,
            proposal!.orderId,
            proposal!.jobId,
            proposal!.statusId,
            WorkStatusEnum.Pending,
            0);

        await _workInOrderRepository.Create(transitModel);
        _logger.LogInformation("Creating works in WiO success");

        await _partSetRepository.MoveFromProposalToOrder(id, proposal.orderId);
        _logger.LogInformation("Mooving parts success");

        await _billRepository.RecalculateAmmount(proposal.orderId);
        _logger.LogInformation("Bill recalculating success");

        await _workProposalRepository.Delete(id); // временно
        _logger.LogInformation("Accepting proposal success");

        return Id;
    }

    public async Task<long> RejectProposal(long id)
    {
        _logger.LogInformation("Rejecting proposal start");

        var Id = await _workProposalRepository.RejectProposal(id);

        await _partSetRepository.DeleteProposedParts(id);
        _logger.LogInformation("Deleting parts success");

        await _workProposalRepository.Delete(id); // временно
        _logger.LogInformation("Deleting proposal success");

        _logger.LogInformation("Accepting proposal success");

        return Id;
    }
}
