namespace CRMSystem.Core.DTOs.Worker;

public record WorkerFilter
(
    string? SortBy,
    int Page,
    int Limit,
    bool isDescending
);
