namespace Shared.Contracts.Work;

public record WorkUpdateRequest
(
    string? Title,
    string? Category,
    string? Description,
    decimal? StandartTime
);
