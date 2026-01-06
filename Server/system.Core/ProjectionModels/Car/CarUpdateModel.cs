using CRMSystem.Core.Enums;

namespace CRMSystem.Core.ProjectionModels.Car;

public record CarUpdateModel
(
    CarStatusEnum? StatusId,
    string? Brand,
    string? Model,
    int? YearOfManufacture,
    int? Mileage
);
