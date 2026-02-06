using Shared.Enums;
using System.Text.Json.Serialization;

namespace Shared.Contracts.Absence;

public record AbsenceRequest
{

    [JsonPropertyName("workerId")]
    public int WorkerId { get; init; }

    [JsonPropertyName("typeId")]
    public AbsenceTypeEnum TypeId { get; init; }

    [JsonPropertyName("startDate")]
    public DateOnly StartDate { get; init; }

    [JsonPropertyName("EndDate")]
    public DateOnly? EndDate { get; init; }
};
