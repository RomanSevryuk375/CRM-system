using CRM.Shared.Abstractions.DDD.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CRM.Shared.Infrastructure.Data.Converters;

public sealed class TaxRateConverter : ValueConverter<TaxRate, decimal>
{
    public TaxRateConverter() : base(
        vo => vo.Value,
        dbVal => new TaxRate(dbVal),
        new ConverterMappingHints(precision: TaxRate.Precision, scale: TaxRate.Scale))
    {
    }
}
