namespace CRMSystem.Core.ProjectionModels.AbsenceType;

public record AbsenceTypeItem
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
};
