namespace CRM_system_backend.Contracts.Part;

public record PartRequest
(
    int CategoryId,
    string? OemArticle,
    string? ManufacturerArticle,
    string InternalArticle,
    string? Description,
    string Name,
    string Manufacturer,
    string Applicability
);
