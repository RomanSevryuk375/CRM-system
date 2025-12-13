using CRMSystem.Core.Enums;

namespace CRMSystem.Core.DTOs.Absence;

public record AbsenceUpdateModel
(
    int id,
    AbsenceTypeEnum? typeId,
    DateOnly? startDate,
    DateOnly? endDate
);