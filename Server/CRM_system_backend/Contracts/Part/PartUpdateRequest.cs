namespace CRMSystem.Core.DTOs.Part;

public record PartUpdateRequest
(
    string? OemArticle,
    string? ManufacturerArticle,
    string? InternalArticle,
    string? Description,
    string? Name,
    string? manufacturer,
    string? Applicability
);
