namespace CRMSystem.Core.ProjectionModels.Absence;

public record AbsenceItem
{
    public int Id { get; init; }
    public string WorkerName { get; init; } = string.Empty;
    public int WorkerId { get; init; }
    public string TypeName { get; init; } = string.Empty;
    public int TypeId { get; set; }
    public DateOnly StartDate { get; init; }
    public DateOnly? EndDate { get; init; }
}
