using CRMSystem.Core.Enums;

namespace CRM_system_backend.Contracts.WorkPropossal;

public record WorkProposalRequest
(
    long Id,
    long OrderId,
    long JobId,
    int WorkerId,
    ProposalStatusEnum StatusId,
    DateTime Date
);
