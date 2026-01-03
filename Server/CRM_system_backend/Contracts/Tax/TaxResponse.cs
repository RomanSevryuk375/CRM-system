namespace CRM_system_backend.Contracts.Tax;

public record TaxResponse
(
    int Id,
    string Name,
    decimal Rate,
    string Type
);
