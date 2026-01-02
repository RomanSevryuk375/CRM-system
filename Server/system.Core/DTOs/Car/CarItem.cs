namespace CRMSystem.Core.DTOs.Car;

public record CarItem
(
    long Id,
    string Owner,
    string Status,
    int StatusId,
    string Brand,
    string Model,
    int YearOfManufacture,
    string VinNumber,
    string StateNumber,
    int Mileage
);
