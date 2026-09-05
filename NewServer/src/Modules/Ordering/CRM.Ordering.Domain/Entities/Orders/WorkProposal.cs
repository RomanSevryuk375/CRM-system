using CRM.Ordering.Domain.Enums;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD;
using CRM.Shared.Abstractions.Results;

namespace CRM.Ordering.Domain.Entities.Orders;

public sealed class WorkProposal : Entity<WorkProposalId>, IAuditable, ISoftDeletable, IHasVersion
{
    private WorkProposal(
        WorkProposalId id,
        OrderId orderId,
        JobId jobId,
        WorkerId workerId,
        WorkProposalStatus status,
        DateTime date)
    {
        Id = id;
        OrderId = orderId;
        JobId = jobId;
        WorkerId = workerId;
        Status = status;
        Date = date;
    }

#pragma warning disable CS8618
    private WorkProposal() { }
#pragma warning restore CS8618

    public OrderId OrderId { get; private set; }
    public JobId JobId { get; private set; }
    public WorkerId WorkerId { get; private set; }
    public WorkProposalStatus Status { get; private set; }
    public DateTime Date { get; private set; }

#pragma warning disable S1144
    public bool IsDeleted { get; private set; }
    public DateTimeOffset? DeletedAt { get; private set; }
    public Guid? DeletedBy { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }
    public Guid CreatedBy { get; private set; }
    public DateTimeOffset? UpdatedAt { get; private set; }
    public Guid? UpdatedBy { get; private set; }

    public Guid Version { get; private set; }
#pragma warning restore S1144

    public static Result<WorkProposal> Create(
        WorkProposalId id,
        OrderId orderId,
        JobId jobId,
        WorkerId workerId,
        DateTime date)
    {
        WorkProposal proposal = new(
            id,
            orderId,
            jobId,
            workerId,
            WorkProposalStatus.Pending,
            date);

        return Result<WorkProposal>.Success(proposal);
    }

    public Result ChangeStatus(WorkProposalStatus newStatus)
    {
        if (Status == newStatus)
        {
            return Result.Success();
        }

        Status = newStatus;

        return Result.Success();
    }
}
