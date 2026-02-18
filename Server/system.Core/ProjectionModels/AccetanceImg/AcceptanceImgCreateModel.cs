namespace CRMSystem.Core.ProjectionModels.AccetanceImg;

public record AcceptanceImgCreateModel
(
    long AcceptanceId, 
    string FilePath,
    string? Description);