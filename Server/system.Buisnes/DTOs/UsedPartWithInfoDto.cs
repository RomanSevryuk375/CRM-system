namespace CRMSystem.Buisnes.DTOs;

public record UsedPartWithInfoDto
(
    int Id,
    int OrderId,
    string SupplierName,
    string Name,
    string Article,
    decimal Quantity,
    decimal UnitPrice,
    decimal Sum
);

