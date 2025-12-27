namespace CRMSystem.Core.DTOs.Shift;

public record ShiftItem
(
   int id,
   string name,
   TimeOnly startAt,
   TimeOnly endAt
);
