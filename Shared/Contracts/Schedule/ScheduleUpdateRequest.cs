namespace Shared.Contracts.Schedule;

public record ScheduleUpdateRequest
(
    int? ShiftId,
    DateTime? DateTime
);