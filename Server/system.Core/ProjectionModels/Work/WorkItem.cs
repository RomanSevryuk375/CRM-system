namespace CRMSystem.Core.ProjectionModels.Work;

public record WorkItem
{
    public long Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Category { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public decimal StandardTime { get; init; }
};
