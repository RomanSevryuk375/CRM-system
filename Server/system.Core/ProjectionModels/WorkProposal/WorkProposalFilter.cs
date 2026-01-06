namespace CRMSystem.Core.ProjectionModels.WorkProposal;

public record WorkProposalFilter
(
    IEnumerable<long> OrderIds,
    IEnumerable<long> JobIds,
    IEnumerable<long> WorkerIds,
    IEnumerable<int> StatusIds,
    string? SortBy,
    int Page,
    int Limit,
    bool IsDescending
);
