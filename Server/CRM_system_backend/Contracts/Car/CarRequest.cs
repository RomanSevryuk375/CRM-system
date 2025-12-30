using CRMSystem.Core.Enums;

namespace CRM_system_backend.Contracts.Car;

public record CarRequest
(
    long ownerId,
    string status,
    CarStatusEnum statusId,
    string brand,
    string model,
    int yearOfManufacture,
    string vinNumber,
    string stateNumber,
    int mileage
);
