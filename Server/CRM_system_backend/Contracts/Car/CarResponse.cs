namespace CRMSystem.Core.DTOs.Car;

public record CarResponse
{
    public long Id { get; init; }
    public string Owner { get; init; } = string.Empty;
    public string Status { get; init; } = string.Empty;
    public int StatusId { get; init; }
    public string Brand { get; init; } = string.Empty;
    public string Model { get; init; } = string.Empty;
    public int YearOfManufacture { get; init; }
    public string VinNumber { get; init; } = string.Empty;
    public string StateNumber { get; init; } = string.Empty;
    public int Mileage { get; init; }
};
