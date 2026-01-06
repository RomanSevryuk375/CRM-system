using CRMSystem.Core.Enums;

namespace CRM_system_backend.Contracts.Absence;

public record AbsenceRequest
(
    int Id,
    int WorkerId,
    AbsenceTypeEnum TypeId,
    DateOnly StartDate,
    DateOnly? EndDate
);
