namespace CRM_system_backend.Contracts.Shift;

public record ShiftUpdateRequest
(
  string? Name,
  TimeOnly? StartAt,
  TimeOnly? EndAt
);
