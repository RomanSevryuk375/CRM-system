using CRM.Shared.Abstractions.DDD;
using CRM.Shared.Abstractions.Results;

namespace CRM.Billing.Domain.ValueObjects;

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
            return Result<Money>.Failure(Error.Validation<Money>(
                "Value should be positive."));
        }

        return Result<Money>.Success(new Money(rawMoney));
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
}
