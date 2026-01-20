namespace Shared.Contracts.Tax;

public record TaxUpdateRequest
(
    string? Name,
    decimal? Rate
);
