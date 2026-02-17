using Shared.Enums;

namespace CRMSystem.Core.ProjectionModels.Car;

public record CarCreateModel
(
    long OwnerId, 
    CarStatusEnum StatusId,
    string Brand,
    string Model,
    int YearOfManufacture,
    string VinNumber,
    string StateNumber,
    int Mileage);