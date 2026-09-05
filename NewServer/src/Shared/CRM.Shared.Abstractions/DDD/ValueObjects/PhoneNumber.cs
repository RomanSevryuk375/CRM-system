namespace CRM.Shared.Abstractions.DDD.ValueObjects;

using CRM.Shared.Abstractions.Results;
using System.Text.RegularExpressions;

public sealed partial class PhoneNumber : ValueObject
{
    private const string Pattern = @"^\+?[1-9]\d{1,14}$";

    private PhoneNumber(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<PhoneNumber> Create(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result<PhoneNumber>.Failure(Error.Validation<PhoneNumber>(Errors.Empty));
        }

        if (!Regex.IsMatch(value, Pattern))
        {
            return Result<PhoneNumber>.Failure(Error.Validation<PhoneNumber>(Errors.InvalidFormat));
        }

        return Result<PhoneNumber>.Success(new PhoneNumber(value));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static class Errors
    {
        public const string Empty = "Phone number cannot be empty.";
        public const string InvalidFormat = "Phone number is in an invalid format.";
    }
}
