namespace CRM_system_backend.Contracts;

public record WorkRequest
(
    int OrderId,
    int JobId,
    int WorkerId,
    decimal TimeSpent,
    int StatusId
);