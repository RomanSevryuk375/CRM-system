namespace CRM_system_backend.Contracts.Schedule;

public record ScheduleResponse
{
    public int Id { get; init; }
    public string Worker { get; init; } = string.Empty;
    public int WorkerId { get; init; }
    public string Shift { get; init; } = string.Empty;
    public int ShiftId { get; init; }
    public DateTime DateTime { get; init; }
};
