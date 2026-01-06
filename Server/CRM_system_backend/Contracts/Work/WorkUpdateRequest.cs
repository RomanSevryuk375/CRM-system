namespace CRM_system_backend.Contracts.Work;

public record WorkUpdateRequest
(
    string? Title,
    string? Category,
    string? Description,
    decimal? StandartTime
);
