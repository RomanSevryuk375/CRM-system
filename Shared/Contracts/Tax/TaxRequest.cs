using Shared.Enums;

namespace Shared.Contracts.Tax;

public record TaxRequest
(
    string Name,
    decimal Rate,
    TaxTypeEnum TypeId
);