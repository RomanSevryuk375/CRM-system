namespace CRM_system_backend.Contracts.SupplySet;

public record SupplySetUpdateRequest
(
    decimal? Quantity,
    decimal? PurchasePrice
);
