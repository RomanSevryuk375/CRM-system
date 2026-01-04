namespace CRM_system_backend.Contracts;

public record BillStatusResponse
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
};
