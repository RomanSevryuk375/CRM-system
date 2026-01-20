namespace Shared.Contracts.Supply;

public record SupplyRequest
(
    int SupplierId,
    DateOnly Date
);
