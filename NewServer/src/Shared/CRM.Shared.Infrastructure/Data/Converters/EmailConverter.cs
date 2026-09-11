using CRM.Shared.Abstractions.DDD.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CRM.Shared.Infrastructure.Data.Converters;

public sealed class EmailConverter : ValueConverter<Email, string>
{
    public EmailConverter() : base(
        vo => vo.Value,
        dbVal => new Email(dbVal),
        new ConverterMappingHints(size: Email.MaxLength))
    {
    }
}
