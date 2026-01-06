namespace CRMSystem.Core.ProjectionModels.PartSet;

public record PartSetFilter
(
    IEnumerable<long?> OrderIds,
    IEnumerable<long> PositionIds,
    IEnumerable<long?> ProposalIds,
    string? SortBy,
    int Page,
    int Limit,
    bool IsDescending
);
