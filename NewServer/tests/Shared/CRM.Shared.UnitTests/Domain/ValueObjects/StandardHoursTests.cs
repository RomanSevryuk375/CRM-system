using CRM.Shared.Abstractions.DDD.ValueObjects;
using FluentAssertions;
using Xunit;

namespace CRM.Shared.UnitTests.Domain.ValueObjects;

public class StandardHoursTests
{
    [Theory]
    [InlineData(-0.01)]
    [InlineData(-5)]
    public void Create_WithNegativeValue_ReturnsFailure(decimal value)
    {
        // Act
        var result = StandardHours.Create(value);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(StandardHours.Errors.NegativeValue);
    }

    [Theory]
    [InlineData(1000.01)]
    [InlineData(1500)]
    public void Create_ExceedsMaxValue_ReturnsFailure(decimal value)
    {
        // Act
        var result = StandardHours.Create(value);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(StandardHours.Errors.ExceedsLimit);
    }

    [Fact]
    public void Create_WithBoundaryZero_ReturnsSuccess()
    {
        // Act
        var result = StandardHours.Create(StandardHours.MinValue);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Value.Should().Be(0m);
    }

    [Fact]
    public void Create_WithBoundaryMax_ReturnsSuccess()
    {
        // Act
        var result = StandardHours.Create(StandardHours.MaxValue);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Value.Should().Be(1000m);
    }

    [Theory]
    [InlineData(0.5)]
    [InlineData(8.0)]
    [InlineData(40.25)]
    public void Create_WithValidValue_ReturnsSuccess(decimal value)
    {
        // Act
        var result = StandardHours.Create(value);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Value.Should().Be(value);
    }
}
