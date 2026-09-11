using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD.ValueObjects;
using CRM.Shared.Infrastructure.Data.Converters;
using Microsoft.EntityFrameworkCore;

using System.Reflection;

namespace CRM.Shared.Infrastructure.Data.Extensions;

public static class ModelConfigurationBuilderExtensions
{
    public static ModelConfigurationBuilder AddSharedValueConverters(
        this ModelConfigurationBuilder configurationBuilder,
        params Assembly[] additionalAssemblies)
    {
        configurationBuilder.Properties<Money>()
            .HaveConversion<MoneyConverter>()
            .HavePrecision(Money.Precision, Money.Scale);

        configurationBuilder.Properties<Name>()
            .HaveConversion<NameConverter>()
            .HaveMaxLength(Name.MaxLength);

        configurationBuilder.Properties<PhoneNumber>()
            .HaveConversion<PhoneNumberConverter>()
            .HaveMaxLength(PhoneNumber.MaxLength);

        configurationBuilder.Properties<Email>()
            .HaveConversion<EmailConverter>()
            .HaveMaxLength(Email.MaxLength);

        configurationBuilder.Properties<TaxRate>()
            .HaveConversion<TaxRateConverter>()
            .HavePrecision(TaxRate.Precision, TaxRate.Scale);

        configurationBuilder.Properties<FuelLevel>()
            .HaveConversion<FuelLevelConverter>();

        configurationBuilder.Properties<Mileage>()
            .HaveConversion<MileageConverter>();

        configurationBuilder.Properties<StandardHours>()
            .HaveConversion<StandardHoursConverter>()
            .HavePrecision(StandardHours.Precision, StandardHours.Scale);

        var assembliesToScan = new HashSet<Assembly>(additionalAssemblies)
        {
            typeof(IEntityId<>).Assembly
        };

        var idTypes = assembliesToScan
            .SelectMany(a => a.GetTypes())
            .Where(t => !t.IsAbstract && !t.IsGenericTypeDefinition)
            .Select(t => new
            {
                Type = t,
                Interface = Array.Find(t.GetInterfaces(), i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEntityId<>))
            })
            .Where(x => x.Interface != null);

        foreach (var item in idTypes)
        {
            Type valueType = item.Interface!.GetGenericArguments()[0];
            Type converterType = typeof(StronglyTypedIdConverter<,>).MakeGenericType(item.Type, valueType);

            configurationBuilder
                .Properties(item.Type)
                .HaveConversion(converterType);
        }

        return configurationBuilder;
    }

    public static ModelConfigurationBuilder AddSharedValueConverters<TDbContext>(this ModelConfigurationBuilder configurationBuilder)
    {
        return configurationBuilder.AddSharedValueConverters(typeof(TDbContext).Assembly);
    }
}
