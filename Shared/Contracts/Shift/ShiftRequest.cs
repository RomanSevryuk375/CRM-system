using System.Text.Json.Serialization;

namespace Shared.Contracts.Shift;

public record ShiftRequest
{
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("startAt")]
    public TimeOnly StartAt { get; init; }

    [JsonPropertyName("endAt")]
    public TimeOnly EndAt { get; init; }
};
