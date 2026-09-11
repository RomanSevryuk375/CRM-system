using System;
using CRM.Customers.Domain.Entities;
using CRM.Shared.Abstractions.Abstractions;

namespace CRM.TestHelpers;

public static class CarMother
{
    public const string DefaultVin = "1HGCR2F83HA123456";
    public const string DefaultStateNumber = "1234 AB-7";
    public static readonly CustomerId DefaultOwnerId = new(Guid.NewGuid());

    public static Car CreateDefault(
        CarId? id = null,
        CustomerId? ownerId = null,
        string brand = "Toyota",
        string model = "Camry",
        int year = 2020,
        string vin = DefaultVin,
        string stateNumber = DefaultStateNumber,
        int mileage = 50000)
    {
        return Car.Create(
            id ?? new CarId(Guid.NewGuid()),
            ownerId ?? DefaultOwnerId,
            brand,
            model,
            year,
            vin,
            stateNumber,
            mileage).Value;
    }
}
