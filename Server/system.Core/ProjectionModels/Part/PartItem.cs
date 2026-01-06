// Ignore Spelling: Oem

namespace CRMSystem.Core.ProjectionModels.Part;

public record PartItem
{
    public long Id { get; init; }
    public string Category { get; init; } = string.Empty;
    public int CategoryId { get; init; }
    public string? OemArticle { get; init; }
    public string? ManufacturerArticle { get; init; }
    public string InternalArticle { get; init; } = string.Empty;
    public string? Description { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Manufacturer { get; init; } = string.Empty;
    public string Applicability { get; init; } = string.Empty;
};