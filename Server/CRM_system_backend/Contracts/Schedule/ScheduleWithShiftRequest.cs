namespace CRM_system_backend.Contracts.Schedule;

public record ScheduleWithShiftRequest
(
    int id,
    int workerId, 
    int shiftId, 
    DateTime dateTime, 
    string name, 
    TimeOnly startAt, 
    TimeOnly endAt
);
