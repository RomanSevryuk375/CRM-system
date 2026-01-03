using CRMSystem.Core.Enums;

namespace CRMSystem.Core.DTOs.Absence;

public record AbsenceUpdateRequest
(
    AbsenceTypeEnum? TypeId,
    DateOnly? StartDate,
    DateOnly? EndDate
);
