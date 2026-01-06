using CRMSystem.Core.Enums;

namespace CRM_system_backend.Contracts.Car;

public record CarUpdateRequest
(
    CarStatusEnum? StatusId,
    string? Brand,
    string? Model,
    int? YearOfManufacture,
    int? Mileage
);
