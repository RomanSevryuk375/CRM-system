using CRMSystem.Core.Enums;

namespace CRMSystem.Core.DTOs.WorkProposal;

public record WorkProposalItem
(
    long id,
    long orderId, 
    string job,
    string woker,
    string status,
    DateTime date
);
