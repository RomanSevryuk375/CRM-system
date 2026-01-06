namespace CRMSystem.Core.ProjectionModels.Work;

public record WorkUpdateModel
(
    string? Title,
    string? Categoty,
    string? Description,
    decimal? StandartTime
);
