using CRM.TestHelpers;
using CRM.IAM.Domain.Entities;
using CRM.IAM.Domain.Enums;
using CRM.Shared.Abstractions.Abstractions;
using FluentAssertions;
using Xunit;

namespace CRM.IAM.UnitTests.Domain.Entities;

public class UserTests
{
    private const string ValidLogin = "admin_user";
    private const string ValidPasswordHash = "$2a$12$e86g1r3.yA4L2qC1m8N5xeT7v0b5V5a6uP7oP9kK2m1";

    [Fact]
    public void Create_WithValidData_ReturnsSuccess()
    {
        // Arrange
        var userId = new UserId(Guid.NewGuid());

        // Act
        var result = User.Create(userId, Role.Admin, ValidLogin, ValidPasswordHash);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Should().Be(userId);
        result.Value.Role.Should().Be(Role.Admin);
        result.Value.Login.Should().Be(ValidLogin);
        result.Value.PasswordHash.Should().Be(ValidPasswordHash);
    }

    [Theory]
    [MemberData(nameof(EmptyStringData.Values), MemberType = typeof(EmptyStringData))]
    public void Create_WithEmptyLogin_ReturnsFailure(string? login)
    {
        // Act
        var result = User.Create(new UserId(Guid.NewGuid()), Role.Manager, login!, ValidPasswordHash);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(User.Errors.LoginEmpty);
    }

    [Fact]
    public void Create_WithTooLongLogin_ReturnsFailure()
    {
        // Arrange
        var longLogin = new string('a', User.MaxLoginLength + 1);

        // Act
        var result = User.Create(new UserId(Guid.NewGuid()), Role.Worker, longLogin, ValidPasswordHash);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(User.Errors.LoginTooLong);
    }

    [Theory]
    [MemberData(nameof(EmptyStringData.Values), MemberType = typeof(EmptyStringData))]
    public void Create_WithEmptyPasswordHash_ReturnsFailure(string? hash)
    {
        // Act
        var result = User.Create(new UserId(Guid.NewGuid()), Role.Customer, ValidLogin, hash!);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(User.Errors.PasswordEmpty);
    }

    [Fact]
    public void Create_WithTooLongPasswordHash_ReturnsFailure()
    {
        // Arrange
        var longHash = new string('h', User.MaxPasswordHashLength + 1);

        // Act
        var result = User.Create(new UserId(Guid.NewGuid()), Role.Admin, ValidLogin, longHash);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(User.Errors.PasswordTooLong);
    }

    [Fact]
    public void ChangePassword_WithValid_ReturnsSuccess()
    {
        // Arrange
        var user = User.Create(new UserId(Guid.NewGuid()), Role.Worker, ValidLogin, ValidPasswordHash).Value;
        var newHash = "$2a$12$newpasswordhash1234567890abcdef";

        // Act
        var result = user.ChangePassword(newHash);

        // Assert
        result.IsSuccess.Should().BeTrue();
        user.PasswordHash.Should().Be(newHash);
    }

    [Theory]
    [MemberData(nameof(EmptyStringData.Values), MemberType = typeof(EmptyStringData))]
    public void ChangePassword_WithEmpty_ReturnsFailure(string? hash)
    {
        // Arrange
        var user = User.Create(new UserId(Guid.NewGuid()), Role.Worker, ValidLogin, ValidPasswordHash).Value;

        // Act
        var result = user.ChangePassword(hash!);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(User.Errors.PasswordEmpty);
    }

    [Fact]
    public void ChangePassword_WithTooLong_ReturnsFailure()
    {
        // Arrange
        var user = User.Create(new UserId(Guid.NewGuid()), Role.Worker, ValidLogin, ValidPasswordHash).Value;
        var longHash = new string('x', User.MaxPasswordHashLength + 1);

        // Act
        var result = user.ChangePassword(longHash);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(User.Errors.PasswordTooLong);
    }
}

