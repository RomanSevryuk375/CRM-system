namespace CRM_system_backend.Contracts.Car;

public record CarResponse
(
    long id,
    string owner,
    string status,
    int statusId,
    string brand,
    string model,
    int yearOfManufacture,
    string vinNumber,
    string stateNumber,
    int mileage
);
