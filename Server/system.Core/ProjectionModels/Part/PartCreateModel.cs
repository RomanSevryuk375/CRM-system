namespace CRMSystem.Core.ProjectionModels.Part;

public record PartCreateModel
(
    int CategoryId,
    string? OemArticle,
    string? ManufacturerArticle,
    string InternalArticle,
    string? Description,
    string Name,
    string Manufacturer,
    string Applicability);