namespace CRMSystem.Core.DTOs.Position;

public record PositionItem
(
    long id,
    string part, 
    int cellId, 
    decimal purchasePrice, 
    decimal sellingPrice, 
    decimal quantity
);