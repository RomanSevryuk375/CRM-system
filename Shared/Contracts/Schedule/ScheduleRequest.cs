namespace Shared.Contracts.Schedule;

public record ScheduleRequest
(
    int Id,
    int WorkerId,
    int ShiftId,
    DateTime DateTime
);