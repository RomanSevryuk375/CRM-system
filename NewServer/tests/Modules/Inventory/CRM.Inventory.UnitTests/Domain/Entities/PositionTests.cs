using CRM.Inventory.Domain.Entities;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD.ValueObjects;
using FluentAssertions;
using Xunit;

namespace CRM.Inventory.UnitTests.Domain.Entities;

public class PositionTests
{
    private static readonly PartId TestPartId = new(Guid.NewGuid());
    private static readonly StorageCellId TestCellId = new(Guid.NewGuid());
    private static readonly Money Purchase = Money.Create(100m).Value;
    private static readonly Money Selling = Money.Create(150m).Value;

    [Fact]
    public void Create_WithValidData_ReturnsSuccess()
    {
        // Arrange
        var positionId = new PositionId(Guid.NewGuid());

        // Act
        var result = Position.Create(positionId, TestPartId, TestCellId, Purchase, Selling, 10m);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Should().Be(positionId);
        result.Value.PartId.Should().Be(TestPartId);
        result.Value.CellId.Should().Be(TestCellId);
        result.Value.PurchasePrice.Should().Be(Purchase);
        result.Value.SellingPrice.Should().Be(Selling);
        result.Value.Quantity.Should().Be(10m);
    }

    [Fact]
    public void Create_WithNegativeQuantity_ReturnsFailure()
    {
        // Act
        var result = Position.Create(new PositionId(Guid.NewGuid()), TestPartId, TestCellId, Purchase, Selling, -1m);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(Position.Errors.NegativeQuantity);
    }

    [Fact]
    public void UpdateQuantity_Increase_ReturnsSuccess()
    {
        // Arrange
        var position = Position.Create(new PositionId(Guid.NewGuid()), TestPartId, TestCellId, Purchase, Selling, 10m).Value;

        // Act
        var result = position.UpdateQuantity(5m);

        // Assert
        result.IsSuccess.Should().BeTrue();
        position.Quantity.Should().Be(15m);
    }

    [Fact]
    public void UpdateQuantity_DecreaseValid_ReturnsSuccess()
    {
        // Arrange
        var position = Position.Create(new PositionId(Guid.NewGuid()), TestPartId, TestCellId, Purchase, Selling, 10m).Value;

        // Act
        var result = position.UpdateQuantity(-4m);

        // Assert
        result.IsSuccess.Should().BeTrue();
        position.Quantity.Should().Be(6m);
    }

    [Fact]
    public void UpdateQuantity_DecreaseTooMuch_ReturnsFailure()
    {
        // Arrange
        var position = Position.Create(new PositionId(Guid.NewGuid()), TestPartId, TestCellId, Purchase, Selling, 10m).Value;

        // Act
        var result = position.UpdateQuantity(-15m);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(Position.Errors.NotEnoughQuantity);
        position.Quantity.Should().Be(10m);
    }
}
