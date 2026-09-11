using CRM.HR.Domain.Entities;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD.ValueObjects;
using FluentAssertions;
using Xunit;

namespace CRM.HR.UnitTests.Domain.Entities;

public class SpecializationTests
{
    [Fact]
    public void Create_WithValidData_ReturnsSuccess()
    {
        // Arrange
        var id = new SpecializationId(Guid.NewGuid());
        var name = Name.Create("Diagnostics").Value;

        // Act
        var result = Specialization.Create(id, name);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Should().Be(id);
        result.Value.Name.Should().Be(name);
    }

    [Fact]
    public void UpdateName_UpdatesName()
    {
        // Arrange
        var specialization = Specialization.Create(new SpecializationId(Guid.NewGuid()), Name.Create("Old Name").Value).Value;
        var newName = Name.Create("New Name").Value;

        // Act
        var result = specialization.UpdateName(newName);

        // Assert
        result.IsSuccess.Should().BeTrue();
        specialization.Name.Should().Be(newName);
    }
}
