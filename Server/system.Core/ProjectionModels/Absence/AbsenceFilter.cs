namespace CRMSystem.Core.DTOs.Absence;

public record AbsenceFilter
(
    IEnumerable<int>? WorkerIds,
    string? SortBy,
    int Page,
    int Limit,
    bool IsDescending
);
