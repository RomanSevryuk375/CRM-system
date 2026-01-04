namespace CRMSystem.Core.DTOs;

public record OrderStatusItem
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
};
