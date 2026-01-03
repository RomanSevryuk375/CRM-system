namespace CRMSystem.Core.DTOs.Work;

public record WorkFilter
(
    string? SortBy,
    int Page,
    int Limit,
    bool IsDescending
);
