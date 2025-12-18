namespace CRMSystem.Core.DTOs.Worker;

public record WorkerItem
(
    int id, 
    long userId, 
    string name, 
    string surname, 
    decimal hourlyRate, 
    string phoneNumber, 
    string email
);
