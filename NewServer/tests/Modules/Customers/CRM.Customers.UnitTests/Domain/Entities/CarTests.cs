using CRM.TestHelpers;
using CRM.Customers.Domain.Entities;
using CRM.Customers.Domain.Enums;
using CRM.Shared.Abstractions.Abstractions;
using FluentAssertions;
using Xunit;

namespace CRM.Customers.UnitTests.Domain.Entities;

public class CarTests
{
    private const string ValidVin = "1HGCR2F83HA123456";
    private const string ValidStateNumber = "1234 AB-7";
    private static readonly CustomerId OwnerId = new(Guid.NewGuid());

    [Fact]
    public void Create_WithValidData_ReturnsSuccess()
    {
        // Arrange
        var carId = new CarId(Guid.NewGuid());

        // Act
        var result = Car.Create(
            carId,
            OwnerId,
            "Toyota",
            "Camry",
            2020,
            ValidVin,
            ValidStateNumber,
            50000);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Should().Be(carId);
        result.Value.OwnerId.Should().Be(OwnerId);
        result.Value.Status.Should().Be(CarStatus.Active);
        result.Value.Brand.Should().Be("Toyota");
        result.Value.Model.Should().Be("Camry");
        result.Value.YearOfManufacture.Should().Be(2020);
        result.Value.VinNumber.Should().Be(ValidVin);
        result.Value.StateNumber.Should().Be(ValidStateNumber);
        result.Value.Mileage.Should().Be(50000);
    }

    [Fact]
    public void Create_TrimsBrandAndModel()
    {
        // Arrange
        var carId = new CarId(Guid.NewGuid());

        // Act
        var result = Car.Create(
            carId,
            OwnerId,
            "  Toyota  ",
            "  Camry  ",
            2020,
            ValidVin,
            ValidStateNumber,
            50000);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Brand.Should().Be("Toyota");
        result.Value.Model.Should().Be("Camry");
    }

    [Theory]
    [MemberData(nameof(EmptyStringData.Values), MemberType = typeof(EmptyStringData))]
    public void Create_WithEmptyBrand_ReturnsFailure(string? brand)
    {
        // Act
        var result = Car.Create(
            new CarId(Guid.NewGuid()),
            OwnerId,
            brand!,
            "Camry",
            2020,
            ValidVin,
            ValidStateNumber,
            50000);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(Car.Errors.BrandEmpty);
    }

    [Fact]
    public void Create_WithTooLongBrand_ReturnsFailure()
    {
        // Arrange
        var longBrand = new string('b', Car.MaxBrandLength + 1);

        // Act
        var result = Car.Create(
            new CarId(Guid.NewGuid()),
            OwnerId,
            longBrand,
            "Camry",
            2020,
            ValidVin,
            ValidStateNumber,
            50000);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(Car.Errors.BrandTooLong);
    }

    [Theory]
    [MemberData(nameof(EmptyStringData.Values), MemberType = typeof(EmptyStringData))]
    public void Create_WithEmptyModel_ReturnsFailure(string? model)
    {
        // Act
        var result = Car.Create(
            new CarId(Guid.NewGuid()),
            OwnerId,
            "Toyota",
            model!,
            2020,
            ValidVin,
            ValidStateNumber,
            50000);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(Car.Errors.ModelEmpty);
    }

    [Fact]
    public void Create_WithTooLongModel_ReturnsFailure()
    {
        // Arrange
        var longModel = new string('m', Car.MaxModelLength + 1);

        // Act
        var result = Car.Create(
            new CarId(Guid.NewGuid()),
            OwnerId,
            "Toyota",
            longModel,
            2020,
            ValidVin,
            ValidStateNumber,
            50000);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(Car.Errors.ModelTooLong);
    }

    [Fact]
    public void Create_WithYearBefore1900_ReturnsFailure()
    {
        // Act
        var result = Car.Create(
            new CarId(Guid.NewGuid()),
            OwnerId,
            "Ford",
            "Model T",
            1899,
            ValidVin,
            ValidStateNumber,
            50000);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(Car.Errors.InvalidYear);
    }

    [Fact]
    public void Create_WithFutureYear_ReturnsFailure()
    {
        // Arrange
        var futureYear = DateTime.UtcNow.Year + Car.MaxFutureYearsOffset + 1;

        // Act
        var result = Car.Create(
            new CarId(Guid.NewGuid()),
            OwnerId,
            "Toyota",
            "Camry",
            futureYear,
            ValidVin,
            ValidStateNumber,
            50000);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(Car.Errors.InvalidYear);
    }

    [Theory]
    [InlineData("SHORTVIN")]
    [InlineData("1HGCR2F83HA12345!")]
    [InlineData("1HGCR2F83HA12345O")] // contains O
    [InlineData("1HGCR2F83HA12345I")] // contains I
    [InlineData("1HGCR2F83HA12345Q")] // contains Q
    public void Create_WithInvalidVin_ReturnsFailure(string invalidVin)
    {
        // Act
        var result = Car.Create(
            new CarId(Guid.NewGuid()),
            OwnerId,
            "Toyota",
            "Camry",
            2020,
            invalidVin,
            ValidStateNumber,
            50000);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(Car.Errors.InvalidVin);
    }

    [Theory]
    [InlineData("INVALID")]
    [InlineData("123456")]
    [InlineData("1234 ZZ-7")] // ZZ not in ABEIKMHOPCTX
    public void Create_WithInvalidStateNumber_ReturnsFailure(string invalidStateNumber)
    {
        // Act
        var result = Car.Create(
            new CarId(Guid.NewGuid()),
            OwnerId,
            "Toyota",
            "Camry",
            2020,
            ValidVin,
            invalidStateNumber,
            50000);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(Car.Errors.InvalidStateNumber);
    }

    [Fact]
    public void Create_WithNegativeMileage_ReturnsFailure()
    {
        // Act
        var result = Car.Create(
            new CarId(Guid.NewGuid()),
            OwnerId,
            "Toyota",
            "Camry",
            2020,
            ValidVin,
            ValidStateNumber,
            -1);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(Car.Errors.NegativeMileage);
    }

    [Fact]
    public void UpdateMileage_WithHigherValue_ReturnsSuccess()
    {
        // Arrange
        var car = CarMother.CreateDefault(ownerId: OwnerId);

        // Act
        var result = car.UpdateMileage(60000);

        // Assert
        result.IsSuccess.Should().BeTrue();
        car.Mileage.Should().Be(60000);
    }

    [Fact]
    public void UpdateMileage_WithLowerValue_ReturnsFailure()
    {
        // Arrange
        var car = CarMother.CreateDefault(ownerId: OwnerId);

        // Act
        var result = car.UpdateMileage(45000);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(Car.Errors.InvalidNewMileage);
        car.Mileage.Should().Be(50000);
    }

    [Fact]
    public void ChangeStatus_WithNewStatus_ChangesStatus()
    {
        // Arrange
        var car = CarMother.CreateDefault(ownerId: OwnerId);

        // Act
        var result = car.ChangeStatus(CarStatus.InService);

        // Assert
        result.IsSuccess.Should().BeTrue();
        car.Status.Should().Be(CarStatus.InService);
    }

    [Fact]
    public void ChangeStatus_WithSameStatus_ReturnsSuccess()
    {
        // Arrange
        var car = CarMother.CreateDefault(ownerId: OwnerId);

        // Act
        var result = car.ChangeStatus(CarStatus.Active);

        // Assert
        result.IsSuccess.Should().BeTrue();
        car.Status.Should().Be(CarStatus.Active);
    }
}

