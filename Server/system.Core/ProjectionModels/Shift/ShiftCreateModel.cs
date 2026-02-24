namespace CRMSystem.Core.ProjectionModels.Shift;

public record ShiftCreateModel
(
    string Name,
    TimeOnly StartAt,
    TimeOnly EndAt);