namespace CRMSystem.Core.DTOs.AcceptanceImg;

public record AcceptanceImgFilter
(
    IEnumerable<long> AcceptanceIds,
    int Page,
    int Limit
);
