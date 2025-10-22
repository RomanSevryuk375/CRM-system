namespace CRM_system_backend.Contracts;

public record CarRequest
(
    int OwnerId,
    string Brand,
    string Model,
    int YearOfManufacture,
    string VinNumber,
    string StateNumber,
    int Mileage
);
