using System.Text.Json.Serialization;

namespace Shared.Contracts.User;

public record UserResponse
{
    [JsonPropertyName("id")]
    long Id { get; init; }

    [JsonPropertyName("role")]
    string Role { get; init; } = string.Empty;

    [JsonPropertyName("roleId")]
    int RoleId { get; init; }

    [JsonPropertyName("login")]
    string Login { get; init; } = string.Empty;

    [JsonPropertyName("password")]
    string PasswordHash { get; init; } = string.Empty;
};
