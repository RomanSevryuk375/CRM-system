namespace CRM_system_backend.Contracts.AbsenceType;

public record AbsenceTypeResponse
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
};
