using CRMSystem.Core.Enums;

namespace CRM_system_backend.Contracts.Absence;

public record AbsenceRequest
(
    int WorkerId,
    int workerId,
    AbsenceTypeEnum TypeId,
    DateOnly StartDate,
    DateOnly? EndDate
);
