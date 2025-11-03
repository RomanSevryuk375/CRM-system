namespace CRM_system_backend.Contracts;

public record TaxResponse
(
    int Id, 
    string Name, 
    decimal Rate, 
    string Type
);
