namespace CRMSystem.Core.DTOs.AbsenceType;

public record AbsenceTypeItem
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
};
