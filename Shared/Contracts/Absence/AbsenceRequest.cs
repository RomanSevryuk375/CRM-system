using Shared.Enums;

namespace Shared.Contracts.Absence;

public record AbsenceRequest
(
    int Id,
    int WorkerId,
    AbsenceTypeEnum TypeId,
    DateOnly StartDate,
    DateOnly? EndDate
);
