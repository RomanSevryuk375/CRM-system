namespace CRM_system_backend.Contracts.Work;

public record WorkRequest
(
    string title,
    string categoty,
    string description,
    decimal standartTime
);