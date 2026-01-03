using CRMSystem.Core.Enums;

namespace CRMSystem.Core.DTOs.Car;

public record CarUpdateModel
(
    CarStatusEnum? StatusId,
    string? Brand,
    string? Model,
    int? YearOfManufacture,
    int? Mileage
);
