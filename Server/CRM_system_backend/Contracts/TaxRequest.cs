namespace CRM_system_backend.Contracts;

public record TaxRequest
(
    string Name,
    decimal Rate,
    string Type
);