namespace CRM_system_backend.Contracts.Absence;

public record AbsenceResponse
{
    public int Id { get; init; }
    public string WorkerName { get; init; } = string.Empty;
    public int WorkerId { get; init; }
    public string TypeName { get; init; } = string.Empty;
    public int TypeId { get; init; }
    public DateOnly StartDate { get; init; }
    public DateOnly? EndDate { get; init; }
}