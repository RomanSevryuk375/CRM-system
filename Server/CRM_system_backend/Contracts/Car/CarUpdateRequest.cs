using CRMSystem.Core.Enums;

namespace CRM_system_backend.Contracts.Car;

public record CarUpdateRequest
(
    CarStatusEnum? statusId,
    string? brand,
    string? model,
    int? yearOfManufacture,
    int? mileage
);
