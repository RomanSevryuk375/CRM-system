using System.Text.Json.Serialization;

namespace Shared.Contracts.Worker;

public record WorkerRequest
{
    [JsonPropertyName("userId")]
    public long UserId { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("surname")]
    public string Surname { get; init; } = string.Empty;

    [JsonPropertyName("gourlyRate")]
    public decimal HourlyRate { get; init; }

    [JsonPropertyName("phoneNumber")]
    public string PhoneNumber { get; init; } = string.Empty;

    [JsonPropertyName("email")]
    public string Email { get; init; } = string.Empty;
}
