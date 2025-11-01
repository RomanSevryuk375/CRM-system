namespace CRMSystem.Buisnes.DTOs;

public record CarWithOwnerDto
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
