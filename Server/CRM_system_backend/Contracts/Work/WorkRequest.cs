namespace CRM_system_backend.Contracts.Work;

public record WorkRequest
(
    string Title,
    string Category,
    string Description,
    decimal StandartTime
);