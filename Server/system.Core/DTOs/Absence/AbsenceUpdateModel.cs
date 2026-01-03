using CRMSystem.Core.Enums;

namespace CRMSystem.Core.DTOs.Absence;

public record AbsenceUpdateModel
(
    AbsenceTypeEnum? TypeId,
    DateOnly? StartDate,
    DateOnly? EndDate
);