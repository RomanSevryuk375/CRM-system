namespace CRMSystem.Core.DTOs.Shift;

public record ShiftItem
{
   public int Id { get; init; }
   public string Name { get; init; } = string.Empty;
   public TimeOnly StartAt { get; init; }
   public TimeOnly EndAt { get; init; }
};
