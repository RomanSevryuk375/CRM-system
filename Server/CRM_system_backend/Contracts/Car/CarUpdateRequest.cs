using CRMSystem.Core.Enums;

namespace CRMSystem.Core.DTOs.Car;

public record CarUpdateRequest
(
    CarStatusEnum? StatusId,
    string? Brand,
    string? Model,
    int? YearOfManufacture,
    int? Mileage
);
