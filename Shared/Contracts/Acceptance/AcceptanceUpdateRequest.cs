using System.Text.Json.Serialization;

namespace Shared.Contracts.Acceptance;

public record AcceptanceUpdateRequest
{
    [JsonPropertyName("mileage")]
    public int? Mileage { get; init; }

    [JsonPropertyName("fuelLevel")]
    public int? FuelLevel { get; init; }

    [JsonPropertyName("externalDefects")]
    public string? ExternalDefects { get; init; }

    [JsonPropertyName("internalDefects")]
    public string? InternalDefects { get; init; }

    [JsonPropertyName("clientSign")]
    public bool? ClientSign { get; init; }

    [JsonPropertyName("workerSign")]
    public bool? WorkerSign { get; init; }
};
