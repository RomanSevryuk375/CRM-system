namespace CRMSystem.Core.DTOs.AcceptanceImg;

public record AcceptanceImgItem
(
    long id,
    long acceptanceId, 
    string filePath, 
    string? description
);
