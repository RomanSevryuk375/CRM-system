namespace CRMSystem.Core.ProjectionModels.Shift;

public record ShiftUpdateModel
(
  string? Name,
  TimeOnly? StartAt,
  TimeOnly? EndAt
);
