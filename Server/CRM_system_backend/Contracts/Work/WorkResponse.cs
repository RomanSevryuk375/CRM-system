namespace CRM_system_backend.Contracts.Work;

public record WorkResponse
(
    long Id,
    string Title,
    string Categoty,
    string Description,
    decimal StandartTime
);
