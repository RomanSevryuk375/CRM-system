namespace CRM_system_backend.Contracts;

public record WorkTypeRequest
(
    string? Title,
    string? Category,
    string? Description,
    decimal? StandardTime
);
