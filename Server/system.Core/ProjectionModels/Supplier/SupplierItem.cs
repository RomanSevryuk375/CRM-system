namespace CRMSystem.Core.ProjectionModels.Supplier;

public record SupplierItem
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Contacts { get; init; } = string.Empty;
};
