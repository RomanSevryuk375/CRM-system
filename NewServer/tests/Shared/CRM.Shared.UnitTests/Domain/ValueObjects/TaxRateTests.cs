using CRM.Shared.Abstractions.DDD.ValueObjects;
using FluentAssertions;
using Xunit;

namespace CRM.Shared.UnitTests.Domain.ValueObjects;

public class TaxRateTests
{
    [Theory]
    [InlineData(-0.01)]
    [InlineData(-20)]
    public void Create_WithNegativePercentage_ReturnsFailure(decimal percentage)
    {
        // Act
        var result = TaxRate.Create(percentage);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(TaxRate.Errors.NegativeRate);
    }

    [Theory]
    [InlineData(100.01)]
    [InlineData(120)]
    public void Create_WithOver100Percentage_ReturnsFailure(decimal percentage)
    {
        // Act
        var result = TaxRate.Create(percentage);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(TaxRate.Errors.ExceedsMax);
    }

    [Fact]
    public void Create_WithZeroPercentage_ReturnsSuccess()
    {
        // Act
        var result = TaxRate.Create(TaxRate.MinPercentage);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Value.Should().Be(0m);
    }

    [Fact]
    public void Create_With100Percentage_ReturnsSuccess()
    {
        // Act
        var result = TaxRate.Create(TaxRate.MaxPercentage);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Value.Should().Be(1m);
    }

    [Fact]
    public void Create_StoresAsFraction()
    {
        // Act: 20% -> 0.20
        var result = TaxRate.Create(20m);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Value.Should().Be(0.20m);
    }

    [Fact]
    public void CalculateAmount_ReturnsCorrectMoney()
    {
        // Arrange
        var taxRate = TaxRate.Create(20m).Value; // 0.2
        var baseAmount = Money.Create(100m).Value;

        // Act
        var taxAmount = taxRate.CalculateAmount(baseAmount);

        // Assert
        taxAmount.Value.Should().Be(20m);
    }
}
