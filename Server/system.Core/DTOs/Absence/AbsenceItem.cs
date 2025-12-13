using CRMSystem.Core.Enums;

namespace CRMSystem.Core.DTOs.Absence;

public record AbsenceItem
(
    int Id,
    string WorkerName,
    string TypeName,
    DateOnly StartDate,
    DateOnly? EndDate
);
