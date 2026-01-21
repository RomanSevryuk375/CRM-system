using Shared.Enums;
using System.Text.Json.Serialization;

namespace Shared.Contracts.Car;

public record CarUpdateRequest
{
    [JsonPropertyName("statusId")]
    public CarStatusEnum? StatusId { get; init; }

    [JsonPropertyName("brand")]
    public string? Brand { get; init; }

    [JsonPropertyName("model")]
    public string? Model { get; init; }

    [JsonPropertyName("yearOfManufacture")]
    public int? YearOfManufacture { get; init; }

    [JsonPropertyName("mileage")]
    public int? Mileage { get; init; }
};
