namespace CRMSystem.Core.DTOs.Schedule;

public record ScheduleFilter
(
    IEnumerable<long> workerIds,
    IEnumerable<long> shiftIds,
    string? SortBy,
    int Page,
    int Limit,
    bool isDescending
);
