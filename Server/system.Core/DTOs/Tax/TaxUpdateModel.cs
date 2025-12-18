namespace CRMSystem.Core.DTOs.Tax;

public record TaxUpdateModel
(
    string? name,
    decimal? rate
);
