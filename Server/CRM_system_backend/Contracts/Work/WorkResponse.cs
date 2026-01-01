namespace CRM_system_backend.Contracts.Work;

public record WorkResponse
(
    long id,
    string title,
    string categoty,
    string description,
    decimal standartTime
);
