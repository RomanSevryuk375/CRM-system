namespace Shared.Contracts.WorkProposal;

public record WorkProposalResponse
(
    long Id,
    long OrderId,
    string Job,
    long JobId,
    string Woker,
    int WorkerId,
    string Status,
    int StatusId,
    DateTime Date
);
