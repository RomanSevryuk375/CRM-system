using CRM.Inventory.Domain.Entities;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD.ValueObjects;
using FluentAssertions;
using Xunit;

namespace CRM.Inventory.UnitTests.Domain.Entities;

public class PartCategoryTests
{
    [Fact]
    public void Create_WithValidData_ReturnsSuccess()
    {
        // Arrange
        var id = new PartCategoryId(Guid.NewGuid());
        var name = Name.Create("Braking System").Value;

        // Act
        var result = PartCategory.Create(id, name, "Pads, discs, calipers");

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Should().Be(id);
        result.Value.Name.Should().Be(name);
        result.Value.Description.Should().Be("Pads, discs, calipers");
    }

    [Fact]
    public void Create_WithTooLongDescription_ReturnsFailure()
    {
        // Arrange
        var id = new PartCategoryId(Guid.NewGuid());
        var name = Name.Create("Braking System").Value;
        var longDesc = new string('d', PartCategory.MaxDescriptionLength + 1);

        // Act
        var result = PartCategory.Create(id, name, longDesc);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(PartCategory.Errors.DescriptionTooLong);
    }
}
