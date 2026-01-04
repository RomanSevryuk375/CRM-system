namespace CRMSystem.Core.DTOs.Work;

public record WorkItem
(
    long Id,
    string Title,
    string Categoty,
    string Description,
    decimal StandartTime
);
