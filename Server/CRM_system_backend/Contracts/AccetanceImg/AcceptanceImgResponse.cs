namespace CRMSystem.Core.DTOs.AccetanceImg;

public record AcceptanceImgResponse
(
    long Id,
    long AcceptanceId,
    string FilePath,
    string? Description
);
