using System.Text.Json.Serialization;

namespace Shared.Contracts.Schedule;

public record ScheduleResponse
{
    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("worker")]
    public string Worker { get; init; } = string.Empty;

    [JsonPropertyName("workerId")]
    public int WorkerId { get; init; }

    [JsonPropertyName("shift")]
    public string Shift { get; init; } = string.Empty;

    [JsonPropertyName("shiftId")]
    public int ShiftId { get; init; }

    [JsonPropertyName("dateTime")]
    public DateTime DateTime { get; init; }
};
