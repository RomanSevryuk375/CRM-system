namespace CRMSystem.Core.ProjectionModels.WorkInOrderStatus;

public record WorkInOrderStatusItem
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
};
