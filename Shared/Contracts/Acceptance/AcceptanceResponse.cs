using System.Text.Json.Serialization;

namespace Shared.Contracts.Acceptance;

public record AcceptanceResponse
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("orderId")]
    public long OrderId { get; init; }

    [JsonPropertyName("worker")]
    public string Worker { get; init; } = string.Empty;

    [JsonPropertyName("workerId")]
    public int WorkerId { get; init; }

    [JsonPropertyName("createAt")]
    public DateTime CreatedAt { get; init; }

    [JsonPropertyName("mileage")]
    public int Mileage { get; init; }

    [JsonPropertyName("fuelLevel")]
    public int FuelLevel { get; init; }

    [JsonPropertyName("externalDefects")]
    public string? ExternalDefects { get; init; }

    [JsonPropertyName("internalDefects")]
    public string? InternalDefects { get; init; }

    [JsonPropertyName("clientSign")]
    public bool? ClientSign { get; init; }

    [JsonPropertyName("workerSign")]
    public bool? WorkerSign { get; init; }
};
