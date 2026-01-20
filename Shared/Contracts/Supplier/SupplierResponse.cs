namespace Shared.Contracts.Supplier;

public record SupplierResponse
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Contacts { get; init; } = string.Empty;
};
