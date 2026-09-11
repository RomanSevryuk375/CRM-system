using CRM.Shared.Abstractions.DDD.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CRM.Shared.Infrastructure.Data.Converters;

public sealed class StandardHoursConverter : ValueConverter<StandardHours, decimal>
{
    public StandardHoursConverter() : base(
        vo => vo.Value,
        dbVal => new StandardHours(dbVal),
        new ConverterMappingHints(precision: StandardHours.Precision, scale: StandardHours.Scale))
    {
    }
}
