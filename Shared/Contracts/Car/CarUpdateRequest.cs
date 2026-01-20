using Shared.Enums;

namespace Shared.Contracts.Car;

public record CarUpdateRequest
(
    CarStatusEnum? StatusId,
    string? Brand,
    string? Model,
    int? YearOfManufacture,
    int? Mileage
);
