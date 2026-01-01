namespace CRM_system_backend.Contracts.Shift;

public record ShiftResponse
(
   int id,
   string name,
   TimeOnly startAt,
   TimeOnly endAt
);
