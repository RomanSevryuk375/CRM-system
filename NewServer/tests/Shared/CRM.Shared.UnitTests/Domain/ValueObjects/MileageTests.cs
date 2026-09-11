using CRM.Shared.Abstractions.DDD.ValueObjects;
using FluentAssertions;
using Xunit;

namespace CRM.Shared.UnitTests.Domain.ValueObjects;

public class MileageTests
{
    [Theory]
    [InlineData(-1)]
    [InlineData(-100)]
    public void Create_WithNegativeValue_ReturnsFailure(int rawMileage)
    {
        // Act
        var result = Mileage.Create(rawMileage);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(Mileage.Errors.NegativeValue);
    }

    [Fact]
    public void Create_WithZeroValue_ReturnsSuccess()
    {
        // Act
        var result = Mileage.Create(0);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Value.Should().Be(0);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(50000)]
    [InlineData(350000)]
    public void Create_WithPositiveValue_ReturnsSuccess(int rawMileage)
    {
        // Act
        var result = Mileage.Create(rawMileage);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Value.Should().Be(rawMileage);
    }

    [Fact]
    public void Zero_ReturnsMileageWithZeroValue()
    {
        // Act
        var zero = Mileage.Zero();

        // Assert
        zero.Value.Should().Be(0);
    }
}
