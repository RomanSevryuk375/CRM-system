namespace CRMSystem.Core.DTOs.AcceptanceImg;

public record AcceptanceImgItem
(
    long Id,
    long acceptanceId,
    string FilePath,
    string? Description
);
