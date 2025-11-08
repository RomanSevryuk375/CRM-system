namespace CRM_system_backend.Contracts;

public record UsedPartRequest
(
    int OrderId,
    int SupplierId,
    string Name,
    string Article,
    decimal Quantity,
    decimal UnitPrice
);
