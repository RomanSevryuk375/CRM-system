using CRMSystem.Core.Enums;

namespace CRM_system_backend.Contracts.WorkPropossal;

public record WorkProposalRequest
(
    long id,
    long orderId,
    long jobId,
    int workerId,
    ProposalStatusEnum statusId,
    DateTime date
);
