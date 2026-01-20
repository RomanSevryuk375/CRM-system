// Ignore Spelling: vin

using CRMSystem.Core.Constants;
using CRMSystem.Core.Validation;
using Shared.Enums;
using System.Text.RegularExpressions;

namespace CRMSystem.Core.Models;

public class Car
{
    private Car(long id, long ownerId, CarStatusEnum statusId, string brand, string model, int yearOfManufacture, string vinNumber, string stateNumber, int mileage)
    {
        Id = id;
        OwnerId = ownerId;
        StatusId = statusId;
        Brand = brand;
        Model = model;
        YearOfManufacture = yearOfManufacture;
        VinNumber = vinNumber;
        StateNumber = stateNumber;
        Mileage = mileage;
    }
    public long Id { get; }
    public long OwnerId { get; }
    public CarStatusEnum StatusId { get; }
    public string Brand { get; }
    public string Model { get; } 
    public int YearOfManufacture { get; }
    public string VinNumber { get; }
    public string StateNumber { get; }
    public int Mileage { get; }

    public static (Car? car, List<string> errors) Create(long id, long ownerId, CarStatusEnum statusId, string brand, string model, int yearOfManufacture, string vinNumber, string stateNumber, int mileage)
    {
        var errors = new List<string>();

        var idError = DomainValidator.ValidateId(id, "id");
        if (!string.IsNullOrEmpty(idError)) errors.Add(idError);

        var ownerIdError = DomainValidator.ValidateId(ownerId, "ownerId");
        if (!string.IsNullOrEmpty(ownerIdError)) errors.Add(ownerIdError);

        var statusIdError = DomainValidator.ValidateId(statusId, "statusID");
        if (!string.IsNullOrEmpty(statusIdError)) errors.Add(statusIdError);

        var brandError = DomainValidator.ValidateString(brand, ValidationConstants.MAX_BRAND_LENGTH, "brand");
        if (!string.IsNullOrEmpty(brandError)) errors.Add(brandError);

        var modelError = DomainValidator.ValidateString(model, ValidationConstants.MAX_MODEL_LENGTH, "model");
        if (!string.IsNullOrEmpty(modelError)) errors.Add(modelError);

        if (yearOfManufacture < 1900)
            errors.Add("We don't repair old junk");

        var VINError = DomainValidator.ValidateString(vinNumber, ValidationConstants.MAX_VIN_LENGTH, "VIN");
        if (!string.IsNullOrEmpty(VINError)) errors.Add(VINError);

        if (!Regex.IsMatch(vinNumber, @"^[A-HJ-NPR-Z0-9]{17}$"))
            errors.Add("VIN number in invalid format");

        var stateNumberError = DomainValidator.ValidateString(stateNumber, ValidationConstants.MAX_STATE_NUMBER_LENGTH, "stateNumber");
        if (!string.IsNullOrEmpty(stateNumberError)) errors.Add(stateNumberError);

        if (!Regex.IsMatch(stateNumber, @"^(\d{4}\s?[ABEIKMHOPCTX]{2}-[1-7]|[ABEIKMHOPCTX]{2}\s?\d{4}-[1-7]|(TA|TT|TY)\d{4}|E\d{3}[ABEIKMHOPCTX]{2}[1-7])$"))
            errors.Add("State number in invalid format");

        var mileageError = DomainValidator.ValidateId(mileage, "mileage");
        if (!string.IsNullOrEmpty(mileageError)) errors.Add(mileageError);

        if (errors.Any())
            return (null, errors);

        var car = new Car(id, ownerId, statusId, brand, model, yearOfManufacture, vinNumber, stateNumber, mileage);

        return (car, new List<string>());
    }
}
