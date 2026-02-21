using System.Text.Json.Serialization;

namespace Shared.Contracts.Schedule;

public record ScheduleWithShiftRequest
{
    [JsonPropertyName("workerId")]
    public int WorkerId { get; init; }

    [JsonPropertyName("shiftId")]
    public int ShiftId { get; init; }

    [JsonPropertyName("dateTime")]
    public DateTime DateTime { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("startAt")]
    public TimeOnly StartAt { get; init; }

    [JsonPropertyName("endAt")]
    public TimeOnly EndAt { get; init; }
};
