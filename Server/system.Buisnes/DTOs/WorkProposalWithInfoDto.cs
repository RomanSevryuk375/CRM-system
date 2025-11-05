namespace CRMSystem.Buisnes.DTOs;

public record WorkProposalWithInfoDto
(
    int Id,
    int OrderId,
    string WorkName,
    string ByWorker,
    string StatusName,
    string DecisionStatusName,
    DateTime Date
);