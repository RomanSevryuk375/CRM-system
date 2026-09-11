using CRM.Shared.Abstractions.DDD.ValueObjects;
using FluentAssertions;
using Xunit;

namespace CRM.Shared.UnitTests.Domain.ValueObjects;

public class FuelLevelTests
{
    [Theory]
    [InlineData(-1)]
    [InlineData(-50)]
    [InlineData(101)]
    [InlineData(200)]
    public void Create_WithOutOfRangeValue_ReturnsFailure(int fuelLevel)
    {
        // Act
        var result = FuelLevel.Create(fuelLevel);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(FuelLevel.Errors.OutOfRange);
    }

    [Fact]
    public void Create_WithMinBoundaryValue_ReturnsSuccess()
    {
        // Act
        var result = FuelLevel.Create(FuelLevel.MinValue);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Value.Should().Be(0);
    }

    [Fact]
    public void Create_WithMaxBoundaryValue_ReturnsSuccess()
    {
        // Act
        var result = FuelLevel.Create(FuelLevel.MaxValue);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Value.Should().Be(100);
    }

    [Theory]
    [InlineData(25)]
    [InlineData(50)]
    [InlineData(75)]
    public void Create_WithValidValue_ReturnsSuccess(int fuelLevel)
    {
        // Act
        var result = FuelLevel.Create(fuelLevel);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Value.Should().Be(fuelLevel);
    }

    [Fact]
    public void Zero_ReturnsFuelLevelWithZeroValue()
    {
        // Act
        var zero = FuelLevel.Zero();

        // Assert
        zero.Value.Should().Be(0);
    }
}
