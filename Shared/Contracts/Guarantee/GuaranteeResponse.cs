using System.Text.Json.Serialization;

namespace Shared.Contracts.Guarantee;

public record GuaranteeResponse
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("orderId")]
    public long OrderId { get; init; }

    [JsonPropertyName("dateStart")]
    public DateOnly DateStart { get; init; }

    [JsonPropertyName("dateEnd")]
    public DateOnly DateEnd { get; init; }

    [JsonPropertyName("description")]
    public string? Description { get; init; }

    [JsonPropertyName("terms")]
    public string Terms { get; init; } = string.Empty;
};
