namespace CRM_system_backend.Contracts.Schedule;

public record ScheduleResponse
(
    int id,
    string worker,
    int workerId,
    string shift,
    int shiftId,
    DateTime dateTime
);
