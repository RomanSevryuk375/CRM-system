using CRM.Shared.Abstractions.DDD.ValueObjects;
using FluentAssertions;
using Xunit;

namespace CRM.Shared.UnitTests.Domain.ValueObjects;

public class MoneyTests
{
    [Theory]
    [InlineData(-0.01)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void Create_WithNegativeValue_ReturnsFailure(decimal rawMoney)
    {
        // Act
        var result = Money.Create(rawMoney);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(Money.Errors.NegativeValue);
    }

    [Fact]
    public void Create_WithZeroValue_ReturnsSuccess()
    {
        // Act
        var result = Money.Create(0m);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Value.Should().Be(0m);
    }

    [Theory]
    [InlineData(0.01)]
    [InlineData(10.50)]
    [InlineData(1000)]
    public void Create_WithPositiveValue_ReturnsSuccess(decimal rawMoney)
    {
        // Act
        var result = Money.Create(rawMoney);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Value.Should().Be(rawMoney);
    }

    [Fact]
    public void Zero_ReturnsMoneyWithZeroValue()
    {
        // Act
        var zero = Money.Zero();

        // Assert
        zero.Value.Should().Be(0m);
    }

    [Fact]
    public void ToString_ReturnsFormattedString()
    {
        // Arrange
        var money = Money.Create(150.75m).Value;

        // Act
        var str = money.ToString();

        // Assert
        str.Should().Be("150.75 BYN");
    }

    [Fact]
    public void Equals_WithSameValues_ReturnsTrue()
    {
        // Arrange
        var money1 = Money.Create(100m).Value;
        var money2 = Money.Create(100m).Value;

        // Assert
        money1.Should().Be(money2);
    }
}
