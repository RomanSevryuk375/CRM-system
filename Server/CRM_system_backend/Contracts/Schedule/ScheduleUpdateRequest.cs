namespace CRM_system_backend.Contracts.Schedule;

public record ScheduleUpdateRequest
(
    int? shiftId,
    DateTime? dateTime
);