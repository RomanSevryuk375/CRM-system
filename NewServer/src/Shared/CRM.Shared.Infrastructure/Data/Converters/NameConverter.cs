using CRM.Shared.Abstractions.DDD.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CRM.Shared.Infrastructure.Data.Converters;

public sealed class NameConverter : ValueConverter<Name, string>
{
    public NameConverter() : base(
        vo => vo.Value,
        dbVal => new Name(dbVal),
        new ConverterMappingHints(size: Name.MaxLength))
    {
    }
}
