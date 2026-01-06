namespace CRMSystem.Core.ProjectionModels.Position;

public record PositionFilter
(
    IEnumerable<long?> PartIds,
    string? SortBy,
    int Page,
    int Limit,
    bool IsDescending
);
