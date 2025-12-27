namespace CRMSystem.Core.DTOs.Work;

public record WorkItem
(
    long id, 
    string title, 
    string categoty, 
    string description, 
    decimal standartTime
);
