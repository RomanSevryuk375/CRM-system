namespace CRM_system_backend.Contracts.Supply;

public record SupplyRequest
(
    int SupplierId,
    DateOnly Date
);
