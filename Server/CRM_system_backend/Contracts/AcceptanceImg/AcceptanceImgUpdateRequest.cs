namespace CRM_system_backend.Contracts.AcceptanceImg;

public record AcceptanceImgUpdateRequest 
(
  string? filePath, 
  string? description
);
