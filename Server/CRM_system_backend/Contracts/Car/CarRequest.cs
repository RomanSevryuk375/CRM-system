namespace CRM_system_backend.Contracts.Car;

public record CarRequest
(
    long OwnerId, 
    int StatusId,
    string Brand,
    string Model,
    int YearOfManufacture,
    string VinNumber,
    string StateNumber,
    int Mileage
);
