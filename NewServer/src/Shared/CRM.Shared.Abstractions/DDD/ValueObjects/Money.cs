using CRM.Shared.Abstractions.Results;

namespace CRM.Shared.Abstractions.DDD.ValueObjects;

public sealed class Money : ValueObject
{
    private const string Currency = "BYN";

    public decimal Value { get; }

    private Money(decimal value)
    {
        Value = value;
    }

    public static Result<Money> Create(decimal rawMoney)
    {
        if (rawMoney < 0)
        {
            return Result<Money>.Failure(Error.Validation<Money>(Errors.NegativeValue));
        }

        return Result<Money>.Success(
            new Money(rawMoney));
    }

    public static Money Zero()
    {
        return new Money(0);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString()
    {
        return $"{Value} + {Currency}";
    }

    public static class Errors
    {
        public const string NegativeValue = "Value should be positive.";
    }
}
