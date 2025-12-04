namespace CRM_system_backend.Contracts;

public record CarUpdateRequest
(
    string? Brand,
    string? Model,
    int? YearOfManufacture,
    string? VinNumber,
    string? StateNumber,
    int? Mileage
);
