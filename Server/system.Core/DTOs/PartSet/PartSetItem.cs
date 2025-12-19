namespace CRMSystem.Core.DTOs.PartSet;

public record PartSetItem
(
    long id,
    long? orderId, 
    string position,
    long positionId,
    long? proposalId, 
    decimal quantity, 
    decimal soldPrice
);
