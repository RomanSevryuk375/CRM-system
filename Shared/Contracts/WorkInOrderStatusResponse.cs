namespace Shared.Contracts;

public record WorkInOrderStatusResponse
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
};