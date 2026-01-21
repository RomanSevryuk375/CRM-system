using System.Text.Json.Serialization;

namespace Shared.Contracts.AbsenceType;

public record AbsenceTypeRequest
{
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;
};
