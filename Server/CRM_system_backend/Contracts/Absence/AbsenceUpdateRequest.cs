using CRMSystem.Core.Enums;

namespace CRM_system_backend.Contracts.Absence;

public record AbsenceUpdateRequest
(
    AbsenceTypeEnum? typeId,
    DateOnly? startDate,
    DateOnly? endDate
);
