namespace CRM_system_backend.Contracts.Part;

public record PartRequest
(
    int categoryId,
    string? oemArticle,
    string? manufacturerArticle,
    string internalArticle,
    string? description,
    string name,
    string manufacturer,
    string applicability
);
