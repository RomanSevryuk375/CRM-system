namespace CRM_system_backend.Contracts.Tax;

public record TaxUpdateRequest
(
    string? Name,
    decimal? Rate
);
