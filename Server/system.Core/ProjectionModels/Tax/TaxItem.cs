namespace CRMSystem.Core.DTOs.Tax;

public record TaxItem
(
    int Id,
    string Name,
    decimal Rate,
    string Type
);
