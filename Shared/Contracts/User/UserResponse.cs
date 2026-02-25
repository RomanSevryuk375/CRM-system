using System.Text.Json.Serialization;

namespace Shared.Contracts.User;

public record UserResponse
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("role")]
    public string Role { get; init; } = string.Empty;

    [JsonPropertyName("roleId")]
    public int RoleId { get; init; }

    [JsonPropertyName("login")]
    public string Login { get; init; } = string.Empty;

    [JsonPropertyName("password")]
    public string PasswordHash { get; init; } = string.Empty;
};
