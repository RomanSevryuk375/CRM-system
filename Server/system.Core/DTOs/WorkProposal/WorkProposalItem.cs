using CRMSystem.Core.Enums;

namespace CRMSystem.Core.DTOs.WorkProposal;

public record WorkProposalItem
(
    long id,
    long orderId, 
    string job,
    long jobId,
    string woker,
    int workerId,
    string status,
    int statusId,
    DateTime date
);
