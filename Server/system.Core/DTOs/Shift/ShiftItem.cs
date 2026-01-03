namespace CRMSystem.Core.DTOs.Shift;

public record ShiftItem
(
   int Id,
   string Name,
   TimeOnly StartAt,
   TimeOnly EndAt
);
