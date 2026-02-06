using System.Text.Json.Serialization;

namespace Shared.Contracts.Schedule;

public record ScheduleRequest
{

    [JsonPropertyName("workerId")]
    public int WorkerId { get; init; }

    [JsonPropertyName("shiftId")]
    public int ShiftId { get; init; }

    [JsonPropertyName("dateTime")]
    public DateTime DateTime { get; init; }
};