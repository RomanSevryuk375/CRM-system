using CRMSystem.Core.Enums;

namespace CRMSystem.Core.ProjectionModels.Absence;

public record AbsenceUpdateModel
{
    public AbsenceTypeEnum? TypeId { get; init; }
    public DateOnly? StartDate { get; init; }
    public DateOnly? EndDate { get; init; }
};