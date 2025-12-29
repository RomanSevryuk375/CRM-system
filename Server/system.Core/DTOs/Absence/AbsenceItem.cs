using CRMSystem.Core.Enums;

namespace CRMSystem.Core.DTOs.Absence;

public record AbsenceItem
(
    int Id,
    string WorkerName,
    int workerId,
    string TypeName,
    int typeId,
    DateOnly StartDate,
    DateOnly? EndDate
);
