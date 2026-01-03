using CRMSystem.Core.Enums;

namespace CRMSystem.Core.DTOs.Position;

public record PositionFilter
(
    IEnumerable<long?> PartIds,
    string? SortBy,
    int Page,
    int Limit,
    bool IsDescending
);
