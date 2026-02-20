namespace CRMSystem.Core.ProjectionModels.OrderStatus;

public record OrderStatusItem
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
};
