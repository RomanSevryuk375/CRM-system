namespace CRM_system_backend.Contracts.Specialization;

public record SpecializationResponse
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
};
