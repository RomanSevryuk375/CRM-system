namespace CRMSystem.Core.DTOs.Part;

public record PartItem
(
    long id,
    string category,
    string? oemArticle,
    string? manufacturerArticle,
    string internalArticle,
    string? description,
    string name,
    string manufacturer,
    string applicability
);