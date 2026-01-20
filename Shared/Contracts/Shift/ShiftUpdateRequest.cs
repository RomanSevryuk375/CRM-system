namespace Shared.Contracts.Shift;

public record ShiftUpdateRequest
(
  string? Name,
  TimeOnly? StartAt,
  TimeOnly? EndAt
);
