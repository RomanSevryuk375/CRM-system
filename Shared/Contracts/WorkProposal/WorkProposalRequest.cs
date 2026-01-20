using Shared.Enums;

namespace Shared.Contracts.WorkPropossal;

public record WorkProposalRequest
(
    long Id,
    long OrderId,
    long JobId,
    int WorkerId,
    ProposalStatusEnum StatusId,
    DateTime Date
);
