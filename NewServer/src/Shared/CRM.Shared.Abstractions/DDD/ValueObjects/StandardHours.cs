using CRM.Shared.Abstractions.Results;

namespace CRM.Shared.Abstractions.DDD.ValueObjects;

public sealed record StandardHours : ValueObject
{
    public const decimal MinValue = 0m;
    public const decimal MaxValue = 1000m;
    public const int Precision = 18;
    public const int Scale = 2;

    public decimal Value { get; }

    internal StandardHours(decimal value)
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

    public static class Errors
    {
        public const string NegativeValue = "Time spent cannot be negative.";
        public const string ExceedsLimit = "Time spent exceeds logical limits.";
    }
}