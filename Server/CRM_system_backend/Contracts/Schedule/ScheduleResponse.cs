namespace CRM_system_backend.Contracts.Schedule;

public record ScheduleResponse
(
    int Id,
    string Worker,
    int WorkerId,
    string Shift,
    int ShiftId,
    DateTime DateTime
);
