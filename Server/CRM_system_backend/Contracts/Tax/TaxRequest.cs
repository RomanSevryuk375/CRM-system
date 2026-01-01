using CRMSystem.Core.Enums;

namespace CRM_system_backend.Contracts.Tax;

public record TaxRequest
(
    string name,
    decimal rate,
    TaxTypeEnum typeId
);