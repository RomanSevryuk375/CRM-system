namespace CRMSystem.Core.DTOs.Schedule;

public record ScheduleFilter
(
    IEnumerable<long> WorkerIds,
    IEnumerable<long> ShiftIds,
    string? SortBy,
    int Page,
    int Limit,
    bool IsDescending
);
