// Ignore Spelling: Vin

using CRM.Customers.Domain.Enums;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD;
using CRM.Shared.Abstractions.Results;
using System.Text.RegularExpressions;

namespace CRM.Customers.Domain.Entities;

public sealed partial class Car : AggregateRoot<CarId>, IAuditable, ISoftDeletable
{
    public const int MaxBrandLength = 100;
    public const int MaxModelLength = 100;
    public const int VinLength = 17;
    public const int MaxStateNumberLength = 32;
    public const int MinYearOfManufacture = 1900;
    public const int MaxFutureYearsOffset = 1;
    public const int MinMileage = 0;

    private Car(
        CarId id,
        CustomerId ownerId,
        CarStatus status,
        string brand,
        string model,
        int yearOfManufacture,
        string vinNumber,
        string stateNumber,
        int mileage)
    {
        Id = id;
        OwnerId = ownerId;
        Status = status;
        Brand = brand;
        Model = model;
        YearOfManufacture = yearOfManufacture;
        VinNumber = vinNumber;
        StateNumber = stateNumber;
        Mileage = mileage;
    }

#pragma warning disable CS8618
    private Car() { }
#pragma warning restore CS8618

    public CustomerId OwnerId { get; private set; }
    public CarStatus Status { get; private set; }
    public string Brand { get; private set; }
    public string Model { get; private set; }
    public int YearOfManufacture { get; private set; }
    public string VinNumber { get; private set; }
    public string StateNumber { get; private set; }
    public int Mileage { get; private set; }

#pragma warning disable S1144
    public bool IsDeleted { get; private set; }
    public DateTimeOffset? DeletedAt { get; private set; }
    public Guid? DeletedBy { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }
    public Guid CreatedBy { get; private set; }
    public DateTimeOffset? UpdatedAt { get; private set; }
    public Guid? UpdatedBy { get; private set; }
#pragma warning restore S1144

    public static Result<Car> Create(
        CarId id,
        CustomerId ownerId,
        string brand,
        string model,
        int yearOfManufacture,
        string vinNumber,
        string stateNumber,
        int mileage)
    {
        if (string.IsNullOrWhiteSpace(brand))
        {
            return Result<Car>.Failure(Error.Validation<Car>(Errors.BrandEmpty));
        }

        if (brand.Length > MaxBrandLength)
        {
            return Result<Car>.Failure(Error.Validation<Car>(Errors.BrandTooLong));
        }

        if (string.IsNullOrWhiteSpace(model))
        {
            return Result<Car>.Failure(Error.Validation<Car>(Errors.ModelEmpty));
        }

        if (model.Length > MaxModelLength)
        {
            return Result<Car>.Failure(Error.Validation<Car>(Errors.ModelTooLong));
        }

        if (yearOfManufacture < MinYearOfManufacture || yearOfManufacture > DateTime.UtcNow.Year + MaxFutureYearsOffset)
        {
            return Result<Car>.Failure(Error.Validation<Car>(Errors.InvalidYear));
        }

        if (!VinRegex().IsMatch(vinNumber))
        {
            return Result<Car>.Failure(Error.Validation<Car>(Errors.InvalidVin));
        }

        if (stateNumber.Length > MaxStateNumberLength || !StateNumberRegex().IsMatch(stateNumber))
        {
            return Result<Car>.Failure(Error.Validation<Car>(Errors.InvalidStateNumber));
        }

        if (mileage < MinMileage)
        {
            return Result<Car>.Failure(Error.Validation<Car>(Errors.NegativeMileage));
        }

        Car car = new(
            id,
            ownerId,
            CarStatus.Active,
            brand.Trim(),
            model.Trim(),
            yearOfManufacture,
            vinNumber.ToUpperInvariant(),
            stateNumber.ToUpperInvariant(),
            mileage);

        car.IncrementVersion();

        return Result<Car>.Success(car);
    }

    public Result UpdateMileage(int newMileage)
    {
        if (newMileage < Mileage)
        {
            return Result.Failure(Error.Validation<Car>(Errors.InvalidNewMileage));
        }

        Mileage = newMileage;
        IncrementVersion();

        return Result.Success();
    }

    public Result ChangeStatus(CarStatus newStatus)
    {
        if (Status == newStatus)
        {
            return Result.Success();
        }

        Status = newStatus;
        IncrementVersion();

        return Result.Success();
    }

    [GeneratedRegex(@"^[A-HJ-NPR-Z0-9]{17}$")]
    private static partial Regex VinRegex();
    [GeneratedRegex(@"^(\d{4}\s?[ABEIKMHOPCTX]{2}-[1-7]|[ABEIKMHOPCTX]{2}\s?\d{4}-[1-7]|(TA|TT|TY)\d{4}|E\d{3}[ABEIKMHOPCTX]{2}[1-7])$")]
    private static partial Regex StateNumberRegex();

    public static class Errors
    {
        public const string BrandEmpty = "Brand cannot be empty.";
        public static readonly string BrandTooLong = $"Brand exceeds maximum length of {MaxBrandLength} characters.";
        public const string ModelEmpty = "Model cannot be empty.";
        public static readonly string ModelTooLong = $"Model exceeds maximum length of {MaxModelLength} characters.";
        public const string InvalidYear = "Invalid year of manufacture.";
        public const string InvalidVin = "VIN number is in an invalid format.";
        public const string InvalidStateNumber = "State number is in an invalid format.";
        public const string NegativeMileage = "Mileage cannot be negative.";
        public const string InvalidNewMileage = "New mileage cannot be less than current mileage.";
    }
}
