using CRMSystem.Core.Enums;

namespace CRMSystem.Core.DTOs.WorkProposal;

public record WorkProposalItem
(
    long Id,
    long OrderId,
    string Job,
    long JobId,
    string Worker,
    int WorkerId,
    string Status,
    int StatusId,
    DateTime Date
);
