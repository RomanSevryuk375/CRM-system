namespace CRM_system_backend.Contracts.Work;

public record WorkUpdateRequest
(
    string? Title,
    string? Categoty,
    string? Description,
    decimal? StandartTime
);
