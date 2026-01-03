namespace CRMSystem.Core.DTOs.Part;

public record PartResponse
(
    long Id,
    string Category,
    int CategoryId,
    string? OemArticle,
    string? ManufacturerArticle,
    string InternalArticle,
    string? Description,
    string Name,
    string Manufacturer,
    string Applicability
);
