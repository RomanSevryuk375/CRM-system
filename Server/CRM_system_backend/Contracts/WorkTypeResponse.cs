namespace CRM_system_backend.Contracts;

public record WorkTypeResponse
(
    int Id,
    string Title,
    string Category,
    string Description,
    decimal StandardTime
);

