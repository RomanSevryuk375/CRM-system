namespace CRM_system_backend.Contracts.Shift;

public record ShiftResponse
(
   int Id,
   string Name,
   TimeOnly StartAt,
   TimeOnly EndAt
);
