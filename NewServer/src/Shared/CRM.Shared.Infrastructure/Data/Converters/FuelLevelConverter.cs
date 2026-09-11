using CRM.Shared.Abstractions.DDD.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CRM.Shared.Infrastructure.Data.Converters;

public sealed class FuelLevelConverter : ValueConverter<FuelLevel, int>
{
    public FuelLevelConverter() : base(
        vo => vo.Value,
        dbVal => new FuelLevel(dbVal))
    {
    }
}
