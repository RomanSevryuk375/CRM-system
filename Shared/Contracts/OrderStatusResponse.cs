namespace Shared.Contracts;

public record OrderStatusResponse
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
};
