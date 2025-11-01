namespace CRM_system_backend.Contracts;

public record CarResponse
(
    int Id,
    int OwnerId,
    string OwnerFullName,
    string Brand,
    string Model,
    int YearOfManufacture,
    string VinNumber,
    string StateNumber,
    int Mileage
);
