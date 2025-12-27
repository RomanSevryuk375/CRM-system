using CRMSystem.Core.Enums;

namespace CRMSystem.Core.DTOs.Car;

public record CarUpdateModel
(
    CarStatusEnum? statusId,
    string? brand,
    string? model,
    int? yearOfManufacture,
    int? mileage
);
