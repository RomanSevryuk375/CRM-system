namespace Shared.Contracts.Work;

public record WorkRequest
(
    string Title,
    string Category,
    string Description,
    decimal StandartTime
);