namespace CRM_system_backend.Contracts.SupplySet;

public record SupplySetRequest
(
    long id,
    long supplyId,
    long positionId,
    decimal quantity,
    decimal purchasePrice
);