namespace CRMSystem.Core.ProjectionModels.CarStatus;

public record CarStatusItem
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
};
