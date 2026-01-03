namespace CRM_system_backend.Contracts.Schedule;

public record ScheduleRequest
(
    int Id,
    int WorkerId,
    int ShiftId,
    DateTime DateTime
);