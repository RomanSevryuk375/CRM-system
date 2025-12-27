namespace CRMSystem.Core.DTOs.PartSet;

public record PartSetFilter
(
    IEnumerable<long?> orderIds,
    IEnumerable<long> positionIds,
    IEnumerable<long?> proposalIds,
    string? SortBy,
    int Page,
    int Limit,
    bool isDescending
);
