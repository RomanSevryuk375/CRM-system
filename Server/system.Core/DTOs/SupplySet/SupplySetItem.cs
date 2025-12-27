namespace CRMSystem.Core.DTOs.SupplySet;

public record SupplySetItem
(
    long id,
    long supplyId,
    string position,
    long positionId,
    decimal quantity,
    decimal purchasePrice
);
