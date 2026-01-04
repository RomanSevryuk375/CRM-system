namespace CRMSystem.Core.DTOs.Work;

public record WorkUpdateModel
(
    string? Title,
    string? Categoty,
    string? Description,
    decimal? StandartTime
);
