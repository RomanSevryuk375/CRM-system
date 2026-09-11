using System;
using CRM.Ordering.Domain.Entities.VehicleInspections;
using CRM.Ordering.Domain.Enums;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD.ValueObjects;

namespace CRM.TestHelpers;

public static class VehicleInspectionMother
{
    public static readonly OrderId DefaultOrderId = new(Guid.NewGuid());
    public static readonly WorkerId DefaultWorkerId = new(Guid.NewGuid());
    public static readonly Mileage DefaultMileage = Mileage.Create(60000).Value;
    public static readonly FuelLevel DefaultFuelLevel = FuelLevel.Create(75).Value;

    public static VehicleInspection CreateDraft(
        VehicleInspectionId? id = null,
        OrderId? orderId = null,
        WorkerId? workerId = null,
        Mileage? mileage = null,
        FuelLevel? fuelLevel = null,
        VehicleCleanliness cleanliness = VehicleCleanliness.Clean,
        bool hasWheelNutKey = true,
        bool hasServiceBook = true,
        string? externalDefects = null,
        string? internalDefects = null,
        string? personalBelongings = null,
        string? dashboardWarnings = null)
    {
        return VehicleInspection.Create(
            id ?? new VehicleInspectionId(Guid.NewGuid()),
            orderId ?? DefaultOrderId,
            workerId ?? DefaultWorkerId,
            mileage ?? DefaultMileage,
            fuelLevel ?? DefaultFuelLevel,
            cleanliness,
            hasWheelNutKey, 
            hasServiceBook, 
            externalDefects, 
            internalDefects, 
            personalBelongings, 
            dashboardWarnings).Value;
    }
}
