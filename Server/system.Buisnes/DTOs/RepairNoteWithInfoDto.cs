namespace CRMSystem.Buisnes.DTOs;

public record RepairNoteWithInfoDto
(
    int Id,
    int OrderId,
    string CarInfo,
    DateTime Date,
    decimal ServiceSum
);
