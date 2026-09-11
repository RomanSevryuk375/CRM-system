using CRM.Shared.Abstractions.Results;

namespace CRM.Shared.Abstractions.DDD.ValueObjects;

public sealed record Money : ValueObject
{
    public const int Precision = 18;
    public const int Scale = 2;
    public const decimal MinValue = 0m;

    private const string Currency = "BYN";

    public decimal Value { get; }

    internal Money(decimal value)
    {
        Value = value;
    }

    public static Result<Money> Create(decimal rawMoney)
    {
        if (rawMoney < MinValue)
        {
            return Result<Money>.Failure(Error.Validation<Money>(Errors.NegativeValue));
        }

        return Result<Money>.Success(
            new Money(rawMoney));
    }

    public static Money Zero()
    {
        return new Money(MinValue);
    }

    public override string ToString()
    {
        return $"{Value} {Currency}";
    }

    public static class Errors
    {
        public const string NegativeValue = "Value should be positive.";
    }
}
