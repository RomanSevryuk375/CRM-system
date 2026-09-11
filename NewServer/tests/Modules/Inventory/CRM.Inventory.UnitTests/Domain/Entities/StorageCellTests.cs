using CRM.TestHelpers;
using CRM.Inventory.Domain.Entities;
using CRM.Shared.Abstractions.Abstractions;
using FluentAssertions;
using Xunit;

namespace CRM.Inventory.UnitTests.Domain.Entities;

public class StorageCellTests
{
    [Fact]
    public void Create_WithValidData_ReturnsSuccess()
    {
        // Arrange
        var id = new StorageCellId(Guid.NewGuid());

        // Act
        var result = StorageCell.Create(id, "A-1", "Level-3");

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Should().Be(id);
        result.Value.Rack.Should().Be("A-1");
        result.Value.Shelf.Should().Be("Level-3");
    }

    [Theory]
    [MemberData(nameof(EmptyStringData.Values), MemberType = typeof(EmptyStringData))]
    public void Create_WithEmptyRack_ReturnsFailure(string? rack)
    {
        // Act
        var result = StorageCell.Create(new StorageCellId(Guid.NewGuid()), rack!, "Shelf-1");

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(StorageCell.Errors.RackEmpty);
    }

    [Fact]
    public void Create_WithTooLongRack_ReturnsFailure()
    {
        // Arrange
        var longRack = new string('r', StorageCell.MaxRackLength + 1);

        // Act
        var result = StorageCell.Create(new StorageCellId(Guid.NewGuid()), longRack, "Shelf-1");

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(StorageCell.Errors.RackTooLong);
    }

    [Theory]
    [MemberData(nameof(EmptyStringData.Values), MemberType = typeof(EmptyStringData))]
    public void Create_WithEmptyShelf_ReturnsFailure(string? shelf)
    {
        // Act
        var result = StorageCell.Create(new StorageCellId(Guid.NewGuid()), "Rack-1", shelf!);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(StorageCell.Errors.ShelfEmpty);
    }

    [Fact]
    public void Create_WithTooLongShelf_ReturnsFailure()
    {
        // Arrange
        var longShelf = new string('s', StorageCell.MaxShelfLength + 1);

        // Act
        var result = StorageCell.Create(new StorageCellId(Guid.NewGuid()), "Rack-1", longShelf);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(StorageCell.Errors.ShelfTooLong);
    }
}

