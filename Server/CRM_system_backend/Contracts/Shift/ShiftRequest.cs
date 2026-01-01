namespace CRM_system_backend.Contracts.Shift;

public record ShiftRequest
(
   string name,
   TimeOnly startAt,
   TimeOnly endAt
);
