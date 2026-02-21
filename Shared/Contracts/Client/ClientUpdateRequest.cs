using System.Text.Json.Serialization;

namespace Shared.Contracts.Client;

public record ClientUpdateRequest
{
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("surname")]
    public string? Surname { get; init; }

    [JsonPropertyName("phoneNumber")]
    public string? PhoneNumber { get; init; }

    [JsonPropertyName("email")]
    public string? Email { get; init; }
};
