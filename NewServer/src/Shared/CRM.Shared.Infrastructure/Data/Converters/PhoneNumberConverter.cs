using CRM.Shared.Abstractions.DDD.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CRM.Shared.Infrastructure.Data.Converters;

public sealed class PhoneNumberConverter : ValueConverter<PhoneNumber, string>
{
    public PhoneNumberConverter() : base(
        vo => vo.Value,
        dbVal => new PhoneNumber(dbVal),
        new ConverterMappingHints(size: PhoneNumber.MaxLength))
    {
    }
}
