namespace Shared.Contracts.Schedule;

public record ScheduleWithShiftRequest
(
    int Id,
    int WorkerId,
    int ShiftId,
    DateTime DateTime,
    string Name,
    TimeOnly StartAt,
    TimeOnly EndAt
);
