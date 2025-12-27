namespace CRMSystem.Core.DTOs.Schedule;

public record ScheduleItem
(
    int id,
    string worker,
    int workerId,
    string shift,
    int shiftId,
    DateTime dateTime
);
