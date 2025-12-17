using CRMSystem.Core.Enums;

namespace CRMSystem.Core.DTOs.Position;

public record PositionFilter
(
    IEnumerable<long?> partIds,
    string? SortBy,
    int Page,
    int Limit,
    bool isDescending
);
