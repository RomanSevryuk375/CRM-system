namespace Shared.Contracts;

public record CarStatusResponse
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
};
