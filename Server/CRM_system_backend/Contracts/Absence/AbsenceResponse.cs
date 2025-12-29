namespace CRM_system_backend.Contracts.Absence;

public record AbsenceResponse
(
    int Id,
    string WorkerName,
    int workerId,
    int TypeName,
    DateOnly StartDate,
    DateOnly? EndDate
);