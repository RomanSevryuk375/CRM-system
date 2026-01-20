using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.WorkProposal;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using Microsoft.Extensions.Logging;
using Shared.Enums;

namespace CRMSystem.Business.Services;

public class WorkProposalService : IWorkProposalService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IWorkRepository _workRepository;
    private readonly IWorkerRepository _workerRepository;
    private readonly IWorkProposalRepository _workProposalRepository;
    private readonly IWorkProposalStatusRepository _workProposalStatusRepository;
    private readonly IWorkInOrderRepository _workInOrderRepository;
    private readonly IPartSetRepository _partSetRepository;
    private readonly IBillRepository _billRepository;
    private readonly ILogger<WorkProposalService> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public WorkProposalService(
        IOrderRepository orderRepository,
        IWorkRepository workRepository,
        IWorkerRepository workerRepository,
        IWorkProposalRepository workProposalRepository,
        IWorkProposalStatusRepository workProposalStatusRepository,
        IWorkInOrderRepository workInOrderRepository,
        IPartSetRepository partSetRepository,
        IBillRepository billRepository,
        ILogger<WorkProposalService> logger,
        IUnitOfWork unitOfWork)
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
        _unitOfWork = unitOfWork;
    }

    public async Task<List<WorkProposalItem>> GetPagedProposals(WorkProposalFilter filter, CancellationToken ct)
    {
        _logger.LogInformation("Getting proposals start");

        var proposals = await _workProposalRepository.GetPaged(filter, ct);

        _logger.LogInformation("Getting proposals success");

        return proposals;
    }

    public async Task<int> GetCountProposals(WorkProposalFilter filter, CancellationToken ct)
    {
        _logger.LogInformation("Getting count proposals start");

        var count = await _workProposalRepository.GetCount(filter, ct);

        _logger.LogInformation("Getting count proposals success");

        return count;
    }

    public async Task<WorkProposalItem> GetProposalById(long id, CancellationToken ct)
    {
        _logger.LogInformation("Getting proposal by id start");

        var proposal = await _workProposalRepository.GetById(id, ct);

        if (proposal is null)
        {
            _logger.LogError("Proposal{proposalId} not found", id);
            throw new NotFoundException($"Proposal{id} not found");
        }

        _logger.LogInformation("Getting proposal by id success");

        return proposal;
    }

    public async Task<long> CreateProposal(WorkProposal workProposal, CancellationToken ct)
    {
        _logger.LogInformation("Creating proposal start");

        if (!await _orderRepository.Exists(workProposal.OrderId, ct))
        {
            _logger.LogError("Order{OrderId} not found", workProposal.OrderId);
            throw new NotFoundException($"Order{workProposal.OrderId} not found");
        }

        if (!await _workProposalStatusRepository.Exists((int)workProposal.StatusId, ct))
        {
            _logger.LogError("Status{StatusId} not found", workProposal.StatusId);
            throw new NotFoundException($"Status{workProposal.StatusId} not found");
        }

        if (!await _workerRepository.Exists(workProposal.WorkerId, ct))
        {
            _logger.LogError("Worker {workerId} not found", workProposal.WorkerId);
            throw new NotFoundException($"Worker {workProposal.WorkerId} not found");
        }

        if (!await _workRepository.Exists(workProposal.JobId, ct))
        {
            _logger.LogError("Work {workId} not found", workProposal.JobId);
            throw new NotFoundException($"Work {workProposal.JobId} not found");
        }

        var Id = await _workProposalRepository.Create(workProposal, ct);

        _logger.LogInformation("Creating proposal success");

        return Id;
    }

    public async Task<long> UpdateProposal(long id, ProposalStatusEnum? statusId, CancellationToken ct)
    {
        _logger.LogInformation("Updating proposal start");

        if (statusId.HasValue && !await _workProposalStatusRepository.Exists((int)statusId.Value, ct))
        {
            _logger.LogError("Status{StatusId} not found", statusId);
            throw new NotFoundException($"Status{statusId} not found");
        }

        var Id = await _workProposalRepository.Update(id, statusId, ct);

        _logger.LogInformation("Updating proposal success");

        return Id;
    }

    public async Task<long> DeleteProposal(long id, CancellationToken ct)
    {
        _logger.LogInformation("Deleting proposal start");

        var Id = await _workProposalRepository.Delete(id, ct);

        _logger.LogInformation("Deleting proposal success");

        return Id;
    }

    public async Task<long> AcceptProposal(long id, CancellationToken ct)
    {
        await _unitOfWork.BeginTransactionAsync(ct);

        try
        {
            _logger.LogInformation("Accepting proposal start");

            var proposal = await _workProposalRepository.GetById(id, ct);
            _logger.LogInformation("Getting proposal by id success");

            if (proposal == null)
                throw new NotFoundException($"Proposal {id} not found");

            var Id = await _workProposalRepository.AcceptProposal(id, ct);

            var transitModel = WorkInOrder.Create(
                0,
                proposal.OrderId,
                proposal.JobId,
                proposal.WorkerId,
                WorkStatusEnum.Pending,
                0).workInOrder!;

            await _workInOrderRepository.Create(transitModel, ct);
            _logger.LogInformation("Creating works in WiO success");

            await _partSetRepository.MoveFromProposalToOrder(id, proposal.OrderId, ct);
            _logger.LogInformation("Moving parts success");

            await _billRepository.RecalculateAmount(proposal.OrderId, ct);
            _logger.LogInformation("Bill recalculating success");

            await _workProposalRepository.Delete(id, ct); 
            _logger.LogInformation("Accepting proposal success");

            await _unitOfWork.CommitTransactionAsync(ct);

            return Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Transaction failed. Rolling back all changes.");

            await _unitOfWork.RollbackAsync(ct);
            throw;
        }
    }

    public async Task<long> RejectProposal(long id, CancellationToken ct)
    {
        await _unitOfWork.BeginTransactionAsync(ct);

        try
        {
            _logger.LogInformation("Rejecting proposal start");

            var Id = await _workProposalRepository.RejectProposal(id, ct);

            await _partSetRepository.DeleteProposedParts(id, ct);
            _logger.LogInformation("Deleting parts success");

            await _workProposalRepository.Delete(id, ct); 
            _logger.LogInformation("Deleting proposal success");

            _logger.LogInformation("Rejecting proposal success");

            await _unitOfWork.CommitTransactionAsync(ct);

            return Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Transaction failed. Rolling back all changes.");

            await _unitOfWork.RollbackAsync(ct);
            throw;
        }
    }
}
