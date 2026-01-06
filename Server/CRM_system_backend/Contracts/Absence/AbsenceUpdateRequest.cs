using CRMSystem.Core.Enums;

namespace CRM_system_backend.Contracts.Absence;

public record AbsenceUpdateRequest
{
    public AbsenceTypeEnum? TypeId { get; init; }
    public DateOnly? StartDate { get; init; }
    public DateOnly? EndDate { get; init; }
};
