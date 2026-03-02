using System.Text.Json.Serialization;

namespace Shared.Contracts.Skill;

public record SkillRequest
{
    [JsonPropertyName("workerId")]
    public int WorkerId { get; init; } 
    
    [JsonPropertyName("specializationId")]
    public int SpecializationId { get; init; } 
}
    