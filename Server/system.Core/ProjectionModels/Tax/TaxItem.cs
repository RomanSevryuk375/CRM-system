namespace CRMSystem.Core.DTOs.Tax;

public record TaxItem
{
    int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public decimal Rate { get; init; }
    public string Type { get; init; } = string.Empty;
};
