namespace CRM_system_backend.Contracts.Tax;

public record TaxResponse
(
    int id,
    string name,
    decimal rate,
    string type
);
