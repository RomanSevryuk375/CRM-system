namespace Shared.Contracts.SupplySet;

public record SupplySetUpdateRequest
(
    decimal? Quantity,
    decimal? PurchasePrice
);
