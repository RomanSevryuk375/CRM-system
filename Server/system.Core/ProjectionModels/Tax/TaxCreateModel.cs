using Shared.Enums;

namespace CRMSystem.Core.ProjectionModels.Tax;

public record TaxCreateModel
(
    string Name,
    decimal Rate,
    TaxTypeEnum TypeId);