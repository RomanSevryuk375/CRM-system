namespace CRMSystem.Buisnes.DTOs;

public record WorkWithInfoDto
(
    int Id, 
    int OrderId,
    string JobName,
    string WorkerInfo,
    decimal TimeSpent,
    string StatusName
);
