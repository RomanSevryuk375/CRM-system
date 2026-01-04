namespace CRMSystem.Core.DTOs.Supply;

public record SupplyItem
(
    long Id,
    string Supplier,
    int SupplierId,
    DateOnly Date
);
