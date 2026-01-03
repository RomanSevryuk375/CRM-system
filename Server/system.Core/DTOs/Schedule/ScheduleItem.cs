namespace CRMSystem.Core.DTOs.Schedule;

public record ScheduleItem
(
    int Id,
    string Worker,
    int WorkerId,
    string Shift,
    int ShiftId,
    DateTime DateTime
);
