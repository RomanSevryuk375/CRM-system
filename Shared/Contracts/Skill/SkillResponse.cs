using System.Text.Json.Serialization;

namespace Shared.Contracts.Skill;

public record SkillResponse
{
    [JsonPropertyName("id")]
    public int Id { get; init; } 
    
    [JsonPropertyName("workerId")]
    public int WorkerId { get; init; }
    
    [JsonPropertyName("worker")]
    public string Worker { get; init; } = string.Empty;
    
    [JsonPropertyName("specializationId")]
    public int SpecializationId { get; init; } 
    
    [JsonPropertyName("specialization")]
    public string Specialization { get; init; } = string.Empty;
}