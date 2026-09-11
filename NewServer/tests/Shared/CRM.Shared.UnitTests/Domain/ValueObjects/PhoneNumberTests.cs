using CRM.TestHelpers;
using CRM.Shared.Abstractions.DDD.ValueObjects;
using FluentAssertions;
using Xunit;

namespace CRM.Shared.UnitTests.Domain.ValueObjects;

public class PhoneNumberTests
{
    [Theory]
    [MemberData(nameof(EmptyStringData.Values), MemberType = typeof(EmptyStringData))]
    public void Create_WithNullOrWhiteSpace_ReturnsFailure(string? phone)
    {
        // Act
        var result = PhoneNumber.Create(phone);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(PhoneNumber.Errors.Empty);
    }

    [Fact]
    public void Create_WithTooLongPhoneNumber_ReturnsFailure()
    {
        // Arrange
        var longPhone = "+" + new string('9', PhoneNumber.MaxLength + 1);

        // Act
        var result = PhoneNumber.Create(longPhone);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(PhoneNumber.Errors.TooLong);
    }

    [Theory]
    [InlineData("abc")]
    [InlineData("+")]
    [InlineData("+012345")]
    [InlineData("123-456-789")]
    [InlineData("+1 234 567")]
    public void Create_WithInvalidFormat_ReturnsFailure(string invalidPhone)
    {
        // Act
        var result = PhoneNumber.Create(invalidPhone);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(PhoneNumber.Errors.InvalidFormat);
    }

    [Theory]
    [InlineData("+375291234567")]
    [InlineData("+14155552671")]
    [InlineData("375291234567")]
    [InlineData("12345")]
    public void Create_WithValidPhoneNumber_ReturnsSuccess(string validPhone)
    {
        // Act
        var result = PhoneNumber.Create(validPhone);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Value.Should().Be(validPhone);
    }
}

