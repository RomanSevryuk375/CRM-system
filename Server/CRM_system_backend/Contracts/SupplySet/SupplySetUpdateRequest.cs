namespace CRM_system_backend.Contracts.SupplySet;

public record SupplySetUpdateRequest
(
    decimal? quantity,
    decimal? purchasePrice
);
