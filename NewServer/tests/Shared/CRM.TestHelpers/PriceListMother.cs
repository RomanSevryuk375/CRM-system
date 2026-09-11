using System;
using CRM.Billing.Domain.Entities;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD.ValueObjects;

namespace CRM.TestHelpers;

public static class PriceListMother
{
    public static readonly DateOnly DefaultValidFrom = new(2026, 1, 1);
    public static readonly Name DefaultName = Name.Create("Standard Price List").Value;

    public static PriceList CreateDefault(
        PriceListId? id = null,
        Name? name = null,
        DateOnly? validFrom = null,
        decimal baseHourlyRate = 50m)
    {
        return PriceList.Create(
            id ?? new PriceListId(Guid.NewGuid()),
            name ?? DefaultName,
            validFrom ?? DefaultValidFrom,
            baseHourlyRate).Value;
    }
}
