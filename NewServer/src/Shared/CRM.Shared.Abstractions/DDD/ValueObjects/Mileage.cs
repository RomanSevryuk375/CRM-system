using CRM.Shared.Abstractions.Results;

namespace CRM.Shared.Abstractions.DDD.ValueObjects;

public sealed class Mileage : ValueObject
{
    public int Value { get; }

    private Mileage(int value)
    {
        Value = value;
    }

    public static Result<Mileage> Create(int rawMileage)
    {
        if (rawMileage < 0)
        {
            return Result<Mileage>.Failure(Error.Validation<Mileage>(Errors.NegativeValue));
        }

        return Result<Mileage>.Success(
            new Mileage(rawMileage));
    }

    public static Mileage Zero()
    {
        return new Mileage(0);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static class Errors
    {
        public const string NegativeValue = "Value should be positive.";
    }
}
