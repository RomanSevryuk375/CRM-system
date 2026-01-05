namespace CRM_system_backend.Contracts.Shift;

public record ShiftResponse
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public TimeOnly StartAt { get; init; }
    public TimeOnly EndAt { get; init; }
};
