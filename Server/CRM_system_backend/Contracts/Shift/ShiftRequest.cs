namespace CRM_system_backend.Contracts.Shift;

public record ShiftRequest
(
   string Name,
   TimeOnly StartAt,
   TimeOnly EndAt
);
