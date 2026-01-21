using System.Text.Json.Serialization;

namespace Shared.Contracts.Schedule;

public record ScheduleRequest
{
    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("workerId")]
    public int WorkerId { get; init; }

    [JsonPropertyName("shiftId")]
    public int ShiftId { get; init; }

    [JsonPropertyName("dateTime")]
    public DateTime DateTime { get; init; }
};