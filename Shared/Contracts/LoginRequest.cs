using System.Text.Json.Serialization;

namespace Shared.Contracts;

public record LoginRequest
{
    [JsonPropertyName("login")]
    public string Login { get; init; } = string.Empty;

    [JsonPropertyName("password")]
    public string Password { get; init; } = string.Empty;
};
