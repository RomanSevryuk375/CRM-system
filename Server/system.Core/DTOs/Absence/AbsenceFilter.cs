namespace CRMSystem.Core.DTOs.Absence;

public record AbsenceFilter
(
    IEnumerable<int>? workerIds,
    string? SortBy,
    int Page,
    int Limit,
    bool isDescending
);
