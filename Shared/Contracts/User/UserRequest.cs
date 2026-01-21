using System.Text.Json.Serialization;

namespace Shared.Contracts.User;

public record UserRequest
{
    [JsonPropertyName("roleId")]
    public int RoleId { get; init; }

    [JsonPropertyName("login")]
    public string Login { get; init; } = string.Empty;

    [JsonPropertyName("password")]
    public string Password { get; init; } = string.Empty;
};

