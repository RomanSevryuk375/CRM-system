namespace CRM_system_backend.Contracts.AcceptanceImg;

public record AcceptanceImgResponse
(
    long id,
    long acceptanceId,
    string filePath,
    string? description
);
