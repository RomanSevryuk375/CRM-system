using System.Text.RegularExpressions;

namespace CRMSystem.Core.Models;

public class Car
{
    public const int MAX_BRAND_LENGTH = 128;
    public const int MAX_MODEL_LENGTH = 256;
    public const int MAX_VIN_LENGTH = 17;
    public const int MAX_STATE_NUMBER_LENGTH = 16;
    public Car(int id, int ownerId, string brand, string model, int yearOfManufacture, string vinNumber, string stateNumber, int mileage)
    {
        Id = id;
        OwnerId = ownerId;
        Brand = brand;
        Model = model;
        YearOfManufacture = yearOfManufacture;
        VinNumber = vinNumber;
        StateNumber = stateNumber;
        Mileage = mileage;
    }
    public int Id { get; }
    public int OwnerId { get; }
    public string Brand { get; } = string.Empty;
    public string Model { get; } = string.Empty;
    public int YearOfManufacture { get; }
    public string VinNumber { get; } = string.Empty;
    public string StateNumber { get; } = string.Empty;
    public int Mileage { get; } 

    public static (Car car,string error) Create(int id, int ownerId, string brand, string model, int yearOfManufacture, string vinNumber, string stateNumber, int mileage)
    {
        var error = string.Empty;

        if (string.IsNullOrWhiteSpace(brand))
            error = "Brand can't be empty";

        if(brand.Length > MAX_BRAND_LENGTH)
            error = $"Brand can't be longer than {MAX_BRAND_LENGTH} symbols";
        if (string.IsNullOrWhiteSpace(model))
            error = "Model can't be empty";

        if (model.Length > MAX_MODEL_LENGTH)
            error = $"Model can't be longer than {MAX_MODEL_LENGTH} symbols";
        if (yearOfManufacture < 1900)
            error = "We don't repair old junk";

        if (string.IsNullOrWhiteSpace(vinNumber))
            error = "VIN number can't be empty";

        if (vinNumber.Length != MAX_VIN_LENGTH)
            error = $"Vin number can't be longer than {MAX_VIN_LENGTH} symbols";

        if (!Regex.IsMatch(vinNumber, @"^[A-HJ-NPR-Z0-9]{17}$"))
            error = "VIN number in invalid format";

        if (string.IsNullOrWhiteSpace(stateNumber))
            error = "State number can't be empty";

        if (stateNumber.Length > MAX_STATE_NUMBER_LENGTH)
            error = $"State number can't be longer than {MAX_STATE_NUMBER_LENGTH} symbols";

        if (!Regex.IsMatch(stateNumber, @"^(\d{4}[ABEIKMHOPCTX]{2}-[1-7]|[ABEIKMHOPCTX]{2}\d{4}-[1-7]|(TA|TT|TY)\d{4}|E\d{3}[ABEIKMHOPCTX]{2}[1-7])$"))
            error = "State number in invalid format";

        if (mileage < 0)
            error = "Milage can't be negative";

        var car = new Car(id, ownerId, brand, model, yearOfManufacture, vinNumber, stateNumber, mileage);

        return (car, error);
    }
}
