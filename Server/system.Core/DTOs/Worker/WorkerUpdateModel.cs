namespace CRMSystem.Core.DTOs.Worker;

public record WorkerUpdateModel
(
    string? name, 
    string? surname, 
    decimal? hourlyRate, 
    string? phoneNumber, 
    string? email
);
