namespace CRMSystem.Core.ProjectionModels.Tax;

public record TaxItem
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public decimal Rate { get; init; }
    public string Type { get; init; } = string.Empty;
};
