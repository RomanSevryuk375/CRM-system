namespace CRM_system_backend.Contracts.WorkPropossal;

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
