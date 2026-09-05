using CRM.Shared.Abstractions.Results;

namespace CRM.Shared.Abstractions.DDD.ValueObjects;

public sealed class Name : ValueObject
{
    public const int MaxLength = 128;

    public string Value { get; }

    private Name(string value)
    {
        Value = value;
    }

    public static Result<Name> Create(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result<Name>.Failure(Error.Validation<Name>(Errors.Empty));
        }

        if (value.Length > MaxLength)
        {
            return Result<Name>.Failure(Error.Validation<Name>(Errors.TooLong));
        }

        return Result<Name>.Success(new Name(value));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static class Errors
    {
        public const string Empty = "Name cannot be empty.";
        public const string TooLong = "Name exceeds maximum length.";
    }
}
