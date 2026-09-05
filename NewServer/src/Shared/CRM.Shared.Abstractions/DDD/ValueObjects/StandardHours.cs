using CRM.Shared.Abstractions.Results;

namespace CRM.Shared.Abstractions.DDD.ValueObjects;

public sealed class StandardHours : ValueObject
{
    private const int MinValue = 0;
    private const int MaxValue = 1000;

    public decimal Value { get; }

    private StandardHours(decimal value)
    {
        Value = value;
    }

    public static Result<StandardHours> Create(decimal value)
    {
        if (value < MinValue)
        {
            return Result<StandardHours>.Failure(Error.Validation<StandardHours>(Errors.NegativeValue));
        }

        if (value > MaxValue)
        {
            return Result<StandardHours>.Failure(Error.Validation<StandardHours>(Errors.ExceedsLimit));
        }

        return Result<StandardHours>.Success(
            new StandardHours(value));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static class Errors
    {
        public const string NegativeValue = "Time spent cannot be negative.";
        public const string ExceedsLimit = "Time spent exceeds logical limits.";
    }
}