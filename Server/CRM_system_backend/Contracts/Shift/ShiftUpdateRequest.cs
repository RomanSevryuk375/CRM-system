namespace CRM_system_backend.Contracts.Shift;

public record ShiftUpdateRequest
(
  string? name,
  TimeOnly? startAt,
  TimeOnly? endAt
);
