// Ignore Spelling: Oem

namespace CRM_system_backend.Contracts.Part;

public record PartResponse
{
    public long Id { get; init; }
    public string Category { get; init; } = string.Empty;
    public int CategoryId { get; init; }
    public string? OEMArticle { get; init; }
    public string? ManufacturerArticle { get; init; }
    public string InternalArticle { get; init; } = string.Empty;
    public string? Description { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Manufacturer { get; init; } = string.Empty;
    public string Applicability { get; init; } = string.Empty;
};
