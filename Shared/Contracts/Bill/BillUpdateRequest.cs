using Shared.Enums;
using System.Text.Json.Serialization;

namespace Shared.Contracts.Bill;

public record BillUpdateRequest
{
    [JsonPropertyName("statusId")]
    public BillStatusEnum? StatusId { get; init; }

    [JsonPropertyName("amount")]
    public decimal? Amount { get; init; }

    [JsonPropertyName("actualBillDate")]
    public DateOnly? ActualBillDate { get; init; }
};
