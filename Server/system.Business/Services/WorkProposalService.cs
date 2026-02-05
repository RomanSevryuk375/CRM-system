using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.WorkProposal;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using Microsoft.Extensions.Logging;
using Shared.Enums;
using Shared.Filters;

namespace CRMSystem.Business.Services;

public class WorkProposalService(
    IOrderRepository orderRepository,
    IWorkRepository workRepository,
    IWorkerRepository workerRepository,
    IWorkProposalRepository workProposalRepository,
    IWorkProposalStatusRepository workProposalStatusRepository,
    IWorkInOrderRepository workInOrderRepository,
    IPartSetRepository partSetRepository,
    IBillRepository billRepository,
    IUserContext userContext,
    ILogger<WorkProposalService> logger,
    IUnitOfWork unitOfWork) : IWorkProposalService
{
    public async Task<List<WorkProposalItem>> GetPagedProposals(WorkProposalFilter filter, CancellationToken ct)
    {
        logger.LogInformation("Getting proposals start");

        if (userContext.RoleId != (int)RoleEnum.Manager)
        {
            filter = filter with { WorkerIds = [(int)userContext.ProfileId] };
        }

        var proposals = await workProposalRepository.GetPaged(filter, ct);

        logger.LogInformation("Getting proposals success");

        return proposals;
    }

    public async Task<int> GetCountProposals(WorkProposalFilter filter, CancellationToken ct)
    {
        logger.LogInformation("Getting count proposals start");

        var count = await workProposalRepository.GetCount(filter, ct);

        logger.LogInformation("Getting count proposals success");

        return count;
    }

    public async Task<WorkProposalItem> GetProposalById(long id, CancellationToken ct)
    {
        logger.LogInformation("Getting proposal by id start");

        var proposal = await workProposalRepository.GetById(id, ct);

        if (proposal is null)
        {
            logger.LogError("Proposal{proposalId} not found", id);
            throw new NotFoundException($"Proposal{id} not found");
        }

        logger.LogInformation("Getting proposal by id success");

        return proposal;
    }

    public async Task<long> CreateProposal(WorkProposal workProposal, CancellationToken ct)
    {
        logger.LogInformation("Creating proposal start");

        if (!await orderRepository.Exists(workProposal.OrderId, ct))
        {
            logger.LogError("Order{OrderId} not found", workProposal.OrderId);
            throw new NotFoundException($"Order{workProposal.OrderId} not found");
        }

        if (!await workProposalStatusRepository.Exists((int)workProposal.StatusId, ct))
        {
            logger.LogError("Status{StatusId} not found", workProposal.StatusId);
            throw new NotFoundException($"Status{workProposal.StatusId} not found");
        }

        if (!await workerRepository.Exists(workProposal.WorkerId, ct))
        {
            logger.LogError("Worker {workerId} not found", workProposal.WorkerId);
            throw new NotFoundException($"Worker {workProposal.WorkerId} not found");
        }

        if (!await workRepository.Exists(workProposal.JobId, ct))
        {
            logger.LogError("Work {workId} not found", workProposal.JobId);
            throw new NotFoundException($"Work {workProposal.JobId} not found");
        }

        var Id = await workProposalRepository.Create(workProposal, ct);

        logger.LogInformation("Creating proposal success");

        return Id;
    }

    public async Task<long> UpdateProposal(long id, ProposalStatusEnum? statusId, CancellationToken ct)
    {
        logger.LogInformation("Updating proposal start");

        if (statusId.HasValue && !await workProposalStatusRepository.Exists((int)statusId.Value, ct))
        {
            logger.LogError("Status{StatusId} not found", statusId);
            throw new NotFoundException($"Status{statusId} not found");
        }

        var Id = await workProposalRepository.Update(id, statusId, ct);

        logger.LogInformation("Updating proposal success");

        return Id;
    }

    public async Task<long> DeleteProposal(long id, CancellationToken ct)
    {
        logger.LogInformation("Deleting proposal start");

        var Id = await workProposalRepository.Delete(id, ct);

        logger.LogInformation("Deleting proposal success");

        return Id;
    }

    public async Task<long> AcceptProposal(long id, CancellationToken ct)
    {
        await unitOfWork.BeginTransactionAsync(ct);

        try
        {
            logger.LogInformation("Accepting proposal start");

            var proposal = await workProposalRepository.GetById(id, ct);
            logger.LogInformation("Getting proposal by id success");

            if (proposal == null)
            {
                throw new NotFoundException($"Proposal {id} not found");
            }

            var Id = await workProposalRepository.AcceptProposal(id, ct);

            var transitModel = WorkInOrder.Create(
                0,
                proposal.OrderId,
                proposal.JobId,
                proposal.WorkerId,
                WorkStatusEnum.Pending,
                0).workInOrder!;

            await workInOrderRepository.Create(transitModel, ct);
            logger.LogInformation("Creating works in WiO success");

            await partSetRepository.MoveFromProposalToOrder(id, proposal.OrderId, ct);
            logger.LogInformation("Moving parts success");

            await billRepository.RecalculateAmount(proposal.OrderId, ct);
            logger.LogInformation("Bill recalculating success");

            await workProposalRepository.Delete(id, ct); 
            logger.LogInformation("Accepting proposal success");

            await unitOfWork.CommitTransactionAsync(ct);

            return Id;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Transaction failed. Rolling back all changes.");

            await unitOfWork.RollbackAsync(ct);
            throw;
        }
    }

    public async Task<long> RejectProposal(long id, CancellationToken ct)
    {
        await unitOfWork.BeginTransactionAsync(ct);

        try
        {
            logger.LogInformation("Rejecting proposal start");

            var Id = await workProposalRepository.RejectProposal(id, ct);

            await partSetRepository.DeleteProposedParts(id, ct);
            logger.LogInformation("Deleting parts success");

            await workProposalRepository.Delete(id, ct); 
            logger.LogInformation("Deleting proposal success");

            logger.LogInformation("Rejecting proposal success");

            await unitOfWork.CommitTransactionAsync(ct);

            return Id;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Transaction failed. Rolling back all changes.");

            await unitOfWork.RollbackAsync(ct);
            throw;
        }
    }
}
