namespace CRMSystem.Core.DTOs.Order;

public record PartUpdateModel
(
    string? OemArticle,
    string? ManufacturerArticle,
    string? InternalArticle,
    string? Description,
    string? Name,
    string? Manufacturer,
    string? Applicability
);
