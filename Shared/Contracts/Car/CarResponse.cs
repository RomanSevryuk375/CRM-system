// Ignore Spelling: Vin

using System.Text.Json.Serialization;

namespace Shared.Contracts.Car;

public record CarResponse
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("owner")]
    public string Owner { get; init; } = string.Empty;

    [JsonPropertyName("status")]
    public string Status { get; init; } = string.Empty;

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
