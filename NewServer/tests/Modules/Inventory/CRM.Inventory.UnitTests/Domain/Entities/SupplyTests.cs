using CRM.Inventory.Domain.Entities;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD.ValueObjects;
using FluentAssertions;
using Xunit;

namespace CRM.Inventory.UnitTests.Domain.Entities;

public class SupplyTests
{
    private static readonly SupplierId TestSupplierId = new(Guid.NewGuid());
    private static readonly DateOnly SupplyDate = new(2026, 9, 10);

    [Fact]
    public void Create_WithValidData_ReturnsSuccess()
    {
        // Arrange
        var id = new SupplyId(Guid.NewGuid());

        // Act
        var result = Supply.Create(id, TestSupplierId, SupplyDate);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Should().Be(id);
        result.Value.SupplierId.Should().Be(TestSupplierId);
        result.Value.Date.Should().Be(SupplyDate);
        result.Value.Items.Should().BeEmpty();
    }

    [Fact]
    public void AddItem_WithValidData_ReturnsSuccess()
    {
        // Arrange
        var supply = Supply.Create(new SupplyId(Guid.NewGuid()), TestSupplierId, SupplyDate).Value;
        var itemId = new SupplyItemId(Guid.NewGuid());
        var positionId = new PositionId(Guid.NewGuid());
        var price = Money.Create(120m).Value;

        // Act
        var result = supply.AddItem(itemId, positionId, 5m, price);

        // Assert
        result.IsSuccess.Should().BeTrue();
        supply.Items.Should().HaveCount(1);
        supply.Items[0].Id.Should().Be(itemId);
        supply.Items[0].PositionId.Should().Be(positionId);
        supply.Items[0].Quantity.Should().Be(5m);
        supply.Items[0].Price.Should().Be(price);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-10)]
    public void AddItem_WithZeroOrNegativeQuantity_ReturnsFailure(decimal qty)
    {
        // Arrange
        var supply = Supply.Create(new SupplyId(Guid.NewGuid()), TestSupplierId, SupplyDate).Value;

        // Act
        var result = supply.AddItem(new SupplyItemId(Guid.NewGuid()), new PositionId(Guid.NewGuid()), qty, Money.Create(100m).Value);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(Supply.Errors.InvalidQuantity);
        supply.Items.Should().BeEmpty();
    }

    [Fact]
    public void RemoveItem_WhenExists_RemovesItem()
    {
        // Arrange
        var supply = Supply.Create(new SupplyId(Guid.NewGuid()), TestSupplierId, SupplyDate).Value;
        var itemId = new SupplyItemId(Guid.NewGuid());
        supply.AddItem(itemId, new PositionId(Guid.NewGuid()), 5m, Money.Create(100m).Value);

        // Act
        var result = supply.RemoveItem(itemId);

        // Assert
        result.IsSuccess.Should().BeTrue();
        supply.Items.Should().BeEmpty();
    }

    [Fact]
    public void RemoveItem_WhenNotFound_ReturnsSuccess()
    {
        // Arrange
        var supply = Supply.Create(new SupplyId(Guid.NewGuid()), TestSupplierId, SupplyDate).Value;

        // Act
        var result = supply.RemoveItem(new SupplyItemId(Guid.NewGuid()));

        // Assert
        result.IsSuccess.Should().BeTrue();
        supply.Items.Should().BeEmpty();
    }
}
