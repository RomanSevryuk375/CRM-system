namespace CRM_system_backend.Contracts.AcceptanceImg;

public record AcceptanceImgRequest
(
    long acceptanceId,
    string filePath,
    string? description
);
