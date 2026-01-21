using System.Text.Json.Serialization;

namespace Shared.Contracts.Car;

public record CarRequest
{
    [JsonPropertyName("ownerId")]
    public long OwnerId { get; init; }

    [JsonPropertyName("statusId")]
    public int StatusId { get; init; }

    [JsonPropertyName("brand")]
    public string Brand { get; init; } = string.Empty;

    [JsonPropertyName("model")]
    public string Model { get; init; } = string.Empty;

    [JsonPropertyName("yearOfManufacture")]
    public int YearOfManufacture { get; init; }

    [JsonPropertyName("vinNumber")]
    public string VinNumber { get; init; } = string.Empty;

    [JsonPropertyName("stateNumber")]
    public string StateNumber { get; init; } = string.Empty;

    [JsonPropertyName("mileage")]
    public int Mileage { get; init; }
};
