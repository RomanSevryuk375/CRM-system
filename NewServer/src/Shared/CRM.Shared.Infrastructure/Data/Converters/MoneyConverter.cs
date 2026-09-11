using CRM.Shared.Abstractions.DDD.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CRM.Shared.Infrastructure.Data.Converters;

public sealed class MoneyConverter : ValueConverter<Money, decimal>
{
    public MoneyConverter() : base(
        vo => vo.Value,
        dbVal => new Money(dbVal),
        new ConverterMappingHints(precision: Money.Precision, scale: Money.Scale))
    {
    }
}
