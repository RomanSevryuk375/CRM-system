namespace CRMSystem.Core.ProjectionModels;

public record SpecializationItem
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
};
