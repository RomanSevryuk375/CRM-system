using Shared.Enums;
using System.Text.Json.Serialization;

namespace Shared.Contracts.Absence;

public record AbsenceUpdateRequest
{
    [JsonPropertyName("typeId")]
    public AbsenceTypeEnum? TypeId { get; init; }

    [JsonPropertyName("startDate")]
    public DateOnly? StartDate { get; init; }

    [JsonPropertyName("endDate")]
    public DateOnly? EndDate { get; init; }
};
