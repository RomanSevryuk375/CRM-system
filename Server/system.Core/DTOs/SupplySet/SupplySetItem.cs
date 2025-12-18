namespace CRMSystem.Core.DTOs.SupplySet;

public record SupplySetItem
(
    long id,
    long supplyId,
    string position,
    decimal quantity,
    decimal purchasePrice
);
