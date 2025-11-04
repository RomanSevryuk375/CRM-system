namespace CRM_system_backend.Contracts;

public record WorkResponse
(
    int Id,
    int OrderId,
    int JobId,
    int WorkerId,
    decimal TimeSpent,
    int StatusId
);
