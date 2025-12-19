namespace CRMSystem.Core.DTOs.Position;

public record PositionItem
(
    long id,
    string part,
    long partId,    
    int cellId, 
    decimal purchasePrice, 
    decimal sellingPrice, 
    decimal quantity
);