namespace CRM.Shared.Abstractions.DDD.ValueObjects;

using CRM.Shared.Abstractions.Results;
using System.Text.RegularExpressions;

public sealed partial record Email : ValueObject
{
    public const int MaxLength = 256;
    private const string Pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

    internal Email(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<Email> Create(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result<Email>.Failure(Error.Validation<Email>(Errors.Empty));
        }

        if (value.Length > MaxLength)
        {
            return Result<Email>.Failure(Error.Validation<Email>(Errors.TooLong));
        }

        if (!MyRegex().IsMatch(value))
        {
            return Result<Email>.Failure(Error.Validation<Email>(Errors.InvalidFormat));
        }

        return Result<Email>.Success(
            new Email(value.ToLowerInvariant()));
    }

    [GeneratedRegex(Pattern)]
    private static partial Regex MyRegex();

    public static class Errors
    {
        public const string Empty = "Email cannot be empty.";
        public const string InvalidFormat = "Email is in an invalid format.";
        public static readonly string TooLong = $"Email exceeds maximum length of {MaxLength} characters.";
    }
}
