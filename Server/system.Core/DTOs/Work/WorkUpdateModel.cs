namespace CRMSystem.Core.DTOs.Work;

public record WorkUpdateModel
(
    string? title,
    string? categoty,
    string? description,
    decimal? standartTime
);
