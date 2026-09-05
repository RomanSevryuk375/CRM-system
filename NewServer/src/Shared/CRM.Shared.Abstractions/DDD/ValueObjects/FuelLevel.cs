using CRM.Shared.Abstractions.Results;

namespace CRM.Shared.Abstractions.DDD.ValueObjects;

public sealed class FuelLevel : ValueObject
{
    public int Value { get; }

    private FuelLevel(int value)
    {
        Value = value;
    }

    public static Result<FuelLevel> Create(int rawFuelLevel)
    {
        if (rawFuelLevel < 0)
        {
            return Result<FuelLevel>.Failure(Error.Validation<FuelLevel>(Errors.NegativeValue));
        }

        return Result<FuelLevel>.Success(
            new(rawFuelLevel));
    }

    public static FuelLevel Zero()
    {
        return new FuelLevel(0);
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
