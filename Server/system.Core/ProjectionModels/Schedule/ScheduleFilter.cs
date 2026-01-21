namespace CRMSystem.Core.ProjectionModels.Schedule;

public record ScheduleFilter
(
    IEnumerable<int> WorkerIds,
    IEnumerable<long> ShiftIds,
    string? SortBy,
    int Page,
    int Limit,
    bool IsDescending
);
