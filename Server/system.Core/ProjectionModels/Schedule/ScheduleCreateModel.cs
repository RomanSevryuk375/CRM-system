namespace CRMSystem.Core.ProjectionModels.Schedule;

public record ScheduleCreateModel
(
    int WorkerId,
    int ShiftId,
    DateTime DateTime);