namespace CRM_system_backend.Contracts.Supply;

public record SupplyResponse
(
    long Id,
    string Supplier,
    int SupplierId,
    DateOnly Date
);
