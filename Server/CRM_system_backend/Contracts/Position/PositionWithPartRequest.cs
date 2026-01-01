namespace CRM_system_backend.Contracts.Position;

public record PositionWithPartRequest
(
    long id,
    long partId, 
    int cellId, 
    decimal purchasePrice, 
    decimal sellingPrice, 
    decimal quantity, 
    int categoryId, 
    string? oemArticle, 
    string? manufacturerArticle, 
    string internalArticle, 
    string? description, 
    string name, 
    string manufacturer, 
    string applicability
);