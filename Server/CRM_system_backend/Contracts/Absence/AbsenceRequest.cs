using CRMSystem.Core.Enums;

namespace CRMSystem.Core.DTOs.Absence;

public record AbsenceRequest
(
    int Id,
    int WorkerId,
    AbsenceTypeEnum TypeId,
    DateOnly StartDate,
    DateOnly? EndDate
);
