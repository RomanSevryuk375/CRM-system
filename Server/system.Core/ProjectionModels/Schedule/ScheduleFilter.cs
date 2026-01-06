namespace CRMSystem.Core.ProjectionModels.Schedule;

public record ScheduleFilter
(
    IEnumerable<long> WorkerIds,
    IEnumerable<long> ShiftIds,
    string? SortBy,
    int Page,
    int Limit,
    bool IsDescending
);
