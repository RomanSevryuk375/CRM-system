namespace CRM.Shared.Abstractions.Results;

public record Error(string Code, string Message, ErrorType Type)
{
    public static readonly Error None = new(string.Empty, string.Empty, ErrorType.Failure);

    public static Error NotFound(string code, string message)
    {
        return new(code, message, ErrorType.NotFound);
    }

    public static Error NotFound<T>(string message)
    {
        return new($"{typeof(T).Name}.NotFound", message, ErrorType.NotFound);
    }

    public static Error Validation(string code, string message)
    {
        return new(code, message, ErrorType.Validation);
    }

    public static Error Validation<T>(string message)
    {
        return new($"{typeof(T).Name}.Invalid", message, ErrorType.Validation);
    }

    public static Error Conflict(string code, string message)
    {
        return new(code, message, ErrorType.Conflict);
    }

    public static Error Conflict<T>(string message)
    {
        return new($"{typeof(T).Name}.Conflict", message, ErrorType.Conflict);
    }

    public static Error Failure(string code, string message)
    {
        return new(code, message, ErrorType.Failure);
    }

    public static Error Failure<T>(string message)
    {
        return new($"{typeof(T).Name}.Failure", message, ErrorType.Failure);
    }

    public static Error AccessDenied(string message)
    {
        return new("Access.Denied", message, ErrorType.Conflict);
    }
}
