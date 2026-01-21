using System.Text.Json.Serialization;

namespace Shared.Contracts.Absence;

public record AbsenceResponse
{
    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("workerName")]
    public string WorkerName { get; init; } = string.Empty;

    [JsonPropertyName("workerId")]
    public int WorkerId { get; init; }

    [JsonPropertyName("typeName")]
    public string TypeName { get; init; } = string.Empty;

    [JsonPropertyName("typeId")]
    public int TypeId { get; init; }

    [JsonPropertyName("startDate")]
    public DateOnly StartDate { get; init; }

    [JsonPropertyName("endDate")]
    public DateOnly? EndDate { get; init; }
}