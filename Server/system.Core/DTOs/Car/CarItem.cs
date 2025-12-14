namespace CRMSystem.Core.DTOs.Car;

public record CarItem
(
    long id,
    string owner,
    string status,
    string brand,
    string model,
    int yearOfManufacture,
    string vinNumber,
    string stateNumber,
    int mileage
);
