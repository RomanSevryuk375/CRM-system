using CRM.Shared.Abstractions.Results;

namespace CRM.Shared.Abstractions.DDD.ValueObjects;

public sealed record FuelLevel : ValueObject
{
    public const int MinValue = 0;
    public const int MaxValue = 100;

    public int Value { get; }

    internal FuelLevel(int value)
    {
        Value = value;
    }

    public static Result<FuelLevel> Create(int rawFuelLevel)
    {
        if (rawFuelLevel < MinValue || rawFuelLevel > MaxValue)
        {
            return Result<FuelLevel>.Failure(Error.Validation<FuelLevel>(Errors.OutOfRange));
        }

        return Result<FuelLevel>.Success(
            new FuelLevel(rawFuelLevel));
    }

    public static FuelLevel Zero()
    {
        return new FuelLevel(MinValue);
    }

    public static class Errors
    {
        public static readonly string OutOfRange = $"Fuel level must be between {MinValue} and {MaxValue} percent.";
    }
}
