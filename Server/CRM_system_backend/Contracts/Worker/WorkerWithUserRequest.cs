namespace CRM_system_backend.Contracts.Worker;

public record WorkerWithUserRequest
(
    string name, 
    string surname, 
    decimal hourlyRate, 
    string phoneNumber, 
    string email,  
    int roleId, 
    string login, 
    string password
);
