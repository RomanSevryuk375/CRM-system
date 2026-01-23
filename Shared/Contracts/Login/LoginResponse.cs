using System.Text.Json.Serialization;

namespace Shared.Contracts.Login;

public record LoginResponse
{
    [JsonPropertyName("token")]
    public string Token { get; init; } = string.Empty;

    [JsonPropertyName("roleId")]
    public int RoleId { get; init; }

    [JsonPropertyName("message")]
    public string Message { get; init; } = string.Empty;
}
