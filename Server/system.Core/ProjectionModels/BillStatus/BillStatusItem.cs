namespace CRMSystem.Core.ProjectionModels.BillStatus;

public record BillStatusItem
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
};
