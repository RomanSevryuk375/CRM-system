namespace CRM_system_backend.Contracts;

public record UsedPartResponse
(
    int Id,
    int OrderId,
    int SupplierId,
    string Name,
    string Article,
    decimal Quantity,
    decimal UnitPrice,
    decimal? Sum
);
