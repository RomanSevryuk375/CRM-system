namespace CRM_system_backend.Contracts.Work;

public record WorkRequest
(
    string Title,
    string Categoty,
    string Description,
    decimal StandartTime
);