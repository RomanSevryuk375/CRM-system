namespace CRM_system_backend.Contracts.Work;

public record WorkUpdateRequest
(
    string? title,
    string? categoty,
    string? description,
    decimal? standartTime
);
