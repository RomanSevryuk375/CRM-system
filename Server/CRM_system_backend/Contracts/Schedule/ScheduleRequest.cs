namespace CRM_system_backend.Contracts.Schedule;

public record ScheduleRequest
(
    int id,
    int workerId,
    int shiftId,
    DateTime dateTime
);