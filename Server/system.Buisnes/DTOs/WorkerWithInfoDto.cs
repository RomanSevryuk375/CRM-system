namespace CRMSystem.Buisnes.DTOs;

public record WorkerWithInfoDto
(
    int Id,
    int UserId,
    string SpecializationName,
    string Name,
    string Surname,
    decimal HourlyRate,
    string PhoneNumber,
    string Email
);
