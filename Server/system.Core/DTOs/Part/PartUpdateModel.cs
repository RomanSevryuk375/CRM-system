namespace CRMSystem.Core.DTOs.Order;

public record PartUpdateModel
(
    string? oemArticle,
    string? manufacturerArticle,
    string? internalArticle,
    string? description,
    string? name,
    string? manufacturer,
    string? applicability
);
