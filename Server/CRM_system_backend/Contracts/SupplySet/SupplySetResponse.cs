namespace CRM_system_backend.Contracts.SupplySet;

public record SupplySetResponse
(
    long id,
    long supplyId,
    string position,
    long positionId,
    decimal quantity,
    decimal purchasePrice
);
