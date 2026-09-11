using CRM.TestHelpers;
using CRM.Shared.Abstractions.DDD.ValueObjects;
using FluentAssertions;
using Xunit;

namespace CRM.Shared.UnitTests.Domain.ValueObjects;

public class EmailTests
{
    [Theory]
    [MemberData(nameof(EmptyStringData.Values), MemberType = typeof(EmptyStringData))]
    public void Create_WithNullOrWhiteSpace_ReturnsFailure(string? email)
    {
        // Act
        var result = Email.Create(email);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(Email.Errors.Empty);
    }

    [Fact]
    public void Create_WithTooLongEmail_ReturnsFailure()
    {
        // Arrange: Email length > 256
        var domain = "@example.com";
        var localPart = new string('a', Email.MaxLength - domain.Length + 1);
        var longEmail = localPart + domain;

        // Act
        var result = Email.Create(longEmail);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(Email.Errors.TooLong);
    }

    [Theory]
    [InlineData("plainaddress")]
    [InlineData("@example.com")]
    [InlineData("email.example.com")]
    [InlineData("email@example@example.com")]
    [InlineData("email@example")]
    [InlineData("email with spaces@example.com")]
    public void Create_WithInvalidFormat_ReturnsFailure(string invalidEmail)
    {
        // Act
        var result = Email.Create(invalidEmail);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(Email.Errors.InvalidFormat);
    }

    [Theory]
    [InlineData("test@example.com")]
    [InlineData("john.doe@sub.company.org")]
    [InlineData("user+tag@example.co.uk")]
    public void Create_WithValidEmail_ReturnsSuccess(string validEmail)
    {
        // Act
        var result = Email.Create(validEmail);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Value.Should().Be(validEmail.ToLowerInvariant());
    }

    [Fact]
    public void Create_ConvertsToLowercase()
    {
        // Arrange
        var upperEmail = "USER@EXAMPLE.COM";

        // Act
        var result = Email.Create(upperEmail);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Value.Should().Be("user@example.com");
    }
}

