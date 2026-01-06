namespace CRM_system_backend.Contracts.Part;

public record PartUpdateRequest
(
    string? OemArticle,
    string? ManufacturerArticle,
    string? InternalArticle,
    string? Description,
    string? Name,
    string? Manufacturer,
    string? Applicability
);
