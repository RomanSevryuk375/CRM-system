namespace CRMSystem.Core.ProjectionModels.Schedule;

public record ScheduleItem
{
    public int Id { get; init; }
    public string Worker { get; init; } = string.Empty;
    public int WorkerId { get; init; }
    public string Shift { get; init; } = string.Empty;
    public int ShiftId { get; init; }
    public DateTime DateTime { get; init; }
};
