namespace CRM_system_backend.Contracts;

public record WorkerResponse
(
    int Id, 
    int UserId,
    int SpecializationId,
    string Name,
    string Surname, 
    decimal HourlyRate,
    string Phonenumber,
    string Email
);
