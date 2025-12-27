namespace CRMSystem.Core.DTOs.AcceptanceImg;

public record AcceptanceImgFilter
(
    IEnumerable<long> acceptanceIds,
    int Page,
    int Limit
);
