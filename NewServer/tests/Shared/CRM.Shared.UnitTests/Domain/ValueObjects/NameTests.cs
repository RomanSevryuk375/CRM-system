using CRM.TestHelpers;
using CRM.Shared.Abstractions.DDD.ValueObjects;
using FluentAssertions;
using Xunit;

namespace CRM.Shared.UnitTests.Domain.ValueObjects;

public class NameTests
{
    [Theory]
    [MemberData(nameof(EmptyStringData.Values), MemberType = typeof(EmptyStringData))]
    public void Create_WithNullOrWhiteSpace_ReturnsFailure(string? name)
    {
        // Act
        var result = Name.Create(name);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(Name.Errors.Empty);
    }

    [Fact]
    public void Create_WithTooLongName_ReturnsFailure()
    {
        // Arrange
        var longName = new string('a', Name.MaxLength + 1);

        // Act
        var result = Name.Create(longName);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(Name.Errors.TooLong);
    }

    [Theory]
    [InlineData("Иван Иванов")]
    [InlineData("John Doe")]
    [InlineData("A")]
    public void Create_WithValidName_ReturnsSuccess(string validName)
    {
        // Act
        var result = Name.Create(validName);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Value.Should().Be(validName);
    }

    [Fact]
    public void Equals_WithSameValues_ReturnsTrue()
    {
        // Arrange
        var name1 = Name.Create("Alexander").Value;
        var name2 = Name.Create("Alexander").Value;

        // Assert
        name1.Should().Be(name2);
    }
}

