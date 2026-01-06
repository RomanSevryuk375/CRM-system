namespace CRMSystem.Core.ProjectionModels;

public record OrderPriorityItem
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
};

