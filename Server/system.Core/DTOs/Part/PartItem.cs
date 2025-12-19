namespace CRMSystem.Core.DTOs.Part;

public record PartItem
(
    long id,
    string category,
    int categoryId,
    string? oemArticle,
    string? manufacturerArticle,
    string internalArticle,
    string? description,
    string name,
    string manufacturer,
    string applicability
);