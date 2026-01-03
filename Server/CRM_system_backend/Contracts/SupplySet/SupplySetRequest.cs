namespace CRM_system_backend.Contracts.SupplySet;

public record SupplySetRequest
(
    long Id,
    long SupplyId,
    long PositionId,
    decimal Quantity,
    decimal PurchasePrice
);