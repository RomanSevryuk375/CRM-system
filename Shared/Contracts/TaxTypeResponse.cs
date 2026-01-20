namespace Shared.Contracts;

public record TaxTypeResponse
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
};
