using CRM.Shared.Abstractions.DDD.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CRM.Shared.Infrastructure.Data.Converters;

public sealed class MileageConverter : ValueConverter<Mileage, int>
{
    public MileageConverter() : base(
        vo => vo.Value,
        dbVal => new Mileage(dbVal))
    {
    }
}
