namespace CRMSystem.Buisnes.DTOs;

public record OrderWithInfoDto
(
    int Id,
    string StatusName, 
    string CarInfo,
    DateTime Date,
    string Priority
);
