namespace CRMSystem.Core.DTOs.Shift;

public record ShiftUpdateModel
(
  string? name, 
  TimeOnly? startAt, 
  TimeOnly? endAt
);
