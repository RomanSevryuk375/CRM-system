namespace CRMSystem.Core.DTOs.Schedule;

public record ScheduleItem
(
    int id,
    string worker,
    string shift,
    DateTime dateTime
);
