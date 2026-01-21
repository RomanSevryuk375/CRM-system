using System.Text.Json.Serialization;

namespace Shared.Contracts.Supplier;

public record SupplierRequest
{
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("contacts")]
    public string Contacts { get; init; } = string.Empty;
}
