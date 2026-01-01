namespace CRM_system_backend.Contracts.Supply;

public record SupplyResponse
(
    long id,
    string supplier,
    int supplierId,
    DateOnly date
);
