using System.Text.Json.Serialization;

namespace Shared.Contracts.Schedule;

public record ScheduleUpdateRequest
{
    [JsonPropertyName("shiftId")]
    public int? ShiftId { get; init; }

    [JsonPropertyName("dateTime")]
    public DateTime? DateTime { get; init; }
};