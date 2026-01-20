namespace Shared.Contracts.Shift;

public record ShiftRequest
(
   string Name,
   TimeOnly StartAt,
   TimeOnly EndAt
);
