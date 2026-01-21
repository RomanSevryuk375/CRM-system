using Shared.Enums;
using System.Text.Json.Serialization;

namespace Shared.Contracts.WorkInOrder;

public record WorkInOrderUpdateRequest
{
    [JsonPropertyName("workerId")]
    public int? WorkerId { get; init; }

    [JsonPropertyName("statusId")]
    public WorkStatusEnum? StatusId { get; init; }

    [JsonPropertyName("timeSpent")]
    public decimal? TimeSpent { get; init; }
};
