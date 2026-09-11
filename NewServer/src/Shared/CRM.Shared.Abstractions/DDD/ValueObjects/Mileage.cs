using CRM.Shared.Abstractions.Results;

namespace CRM.Shared.Abstractions.DDD.ValueObjects;

public sealed record Mileage : ValueObject
{
    public const int MinValue = 0;

    public int Value { get; }

    internal Mileage(int value)
    {
        Value = value;
    }

    public static Result<Mileage> Create(int rawMileage)
    {
        if (rawMileage < MinValue)
        {
            return Result<Mileage>.Failure(Error.Validation<Mileage>(Errors.NegativeValue));
        }

        return Result<Mileage>.Success(
            new Mileage(rawMileage));
    }

    public static Mileage Zero()
    {
        return new Mileage(MinValue);
    }

    public static class Errors
    {
        public const string NegativeValue = "Value should be positive.";
    }
}
