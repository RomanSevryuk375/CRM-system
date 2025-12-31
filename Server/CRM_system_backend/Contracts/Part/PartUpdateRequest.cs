namespace CRM_system_backend.Contracts.Part;

public record PartUpdateRequest
(
    string? oemArticle,
    string? manufacturerArticle,
    string? internalArticle,
    string? description,
    string? name,
    string? manufacturer,
    string? applicability
);
