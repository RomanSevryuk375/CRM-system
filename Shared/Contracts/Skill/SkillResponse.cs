using System.Text.Json.Serialization;

namespace Shared.Contracts.Skill;

public record SkillResponse
{
    [JsonPropertyName("id")]
    public int Id { get; init; } 
    
    [JsonPropertyName("workerId")]
    public int WorkerId { get; init; } 
    
    [JsonPropertyName("specializationId")]
    public int SpecializationId { get; init; } 
}