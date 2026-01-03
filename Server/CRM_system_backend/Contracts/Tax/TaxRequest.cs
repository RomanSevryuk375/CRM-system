using CRMSystem.Core.Enums;

namespace CRM_system_backend.Contracts.Tax;

public record TaxRequest
(
    string Name,
    decimal Rate,
    TaxTypeEnum TypeId
);