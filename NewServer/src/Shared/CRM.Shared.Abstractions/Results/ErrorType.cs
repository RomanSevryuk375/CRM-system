namespace CRM.Shared.Abstractions.Results;

public enum ErrorType
{
    Failure = 0,    // 500 || 400 
    Validation = 1, // 400 
    NotFound = 2,   // 404 
    Conflict = 3    // 409 
}
