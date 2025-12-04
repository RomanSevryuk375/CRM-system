namespace CRM_system_backend.Contracts;

public record WorkProposalRequest
(
    int? OrderId,
    int? WorkId,
    int? ByWorker,
    int? StatusId,
    int? DecisionStatusId,
    DateTime? Date
);
