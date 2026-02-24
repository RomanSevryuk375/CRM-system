using Shared.Enums;

namespace CRMSystem.Core.ProjectionModels.Absence;

public record AbsenceCreateModel
(
    int WorkerId,
    AbsenceTypeEnum TypeId, 
    DateOnly StartDate,
    DateOnly? EndDate);