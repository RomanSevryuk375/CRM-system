namespace CRMSystem.Core.ProjectionModels.TaxType;

public record TaxTypeItem
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
};
