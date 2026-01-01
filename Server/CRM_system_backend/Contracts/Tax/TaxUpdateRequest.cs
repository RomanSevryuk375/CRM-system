namespace CRM_system_backend.Contracts.Tax;

public record TaxUpdateRequest
(
    string? name,
    decimal? rate
);
