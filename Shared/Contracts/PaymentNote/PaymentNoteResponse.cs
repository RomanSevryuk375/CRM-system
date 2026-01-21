using Shared.Enums;
using System.Text.Json.Serialization;

namespace Shared.Contracts.PaymentNote;

public record PaymentNoteResponse
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("billId")]
    public long BillId { get; init; }

    [JsonPropertyName("date")]
    public DateTime Date { get; init; }

    [JsonPropertyName("amount")]
    public decimal Amount { get; init; }

    [JsonPropertyName("methodId")]
    public PaymentMethodEnum MethodId { get; init; }
};
