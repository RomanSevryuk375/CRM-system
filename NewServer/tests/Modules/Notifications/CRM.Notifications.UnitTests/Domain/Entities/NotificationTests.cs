using CRM.TestHelpers;
using CRM.Notifications.Domain.Entities;
using CRM.Notifications.Domain.Enums;
using CRM.Shared.Abstractions.Abstractions;
using FluentAssertions;
using Xunit;

namespace CRM.Notifications.UnitTests.Domain.Entities;

public class NotificationTests
{
    private static readonly CustomerId TestCustomerId = new(Guid.NewGuid());
    private static readonly CarId TestCarId = new(Guid.NewGuid());
    private static readonly DateTime SendTime = new(2026, 9, 11, 10, 0, 0, DateTimeKind.Utc);

    [Fact]
    public void Create_WithValidMessage_ReturnsSuccess()
    {
        // Arrange
        var id = new NotificationId(Guid.NewGuid());

        // Act
        var result = Notification.Create(
            id,
            TestCustomerId,
            TestCarId,
            NotificationType.Sms,
            "Your car is ready for pickup!",
            SendTime);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Should().Be(id);
        result.Value.CustomerId.Should().Be(TestCustomerId);
        result.Value.CarId.Should().Be(TestCarId);
        result.Value.Type.Should().Be(NotificationType.Sms);
        result.Value.Status.Should().Be(NotificationStatus.Pending);
        result.Value.Message.Should().Be("Your car is ready for pickup!");
        result.Value.SendAt.Should().Be(SendTime);
    }

    [Fact]
    public void Create_WithNullCarId_ReturnsSuccess()
    {
        // Act
        var result = Notification.Create(
            new NotificationId(Guid.NewGuid()),
            TestCustomerId,
            carId: null,
            NotificationType.Email,
            "Welcome to our service!",
            SendTime);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.CarId.Should().BeNull();
    }

    [Theory]
    [MemberData(nameof(EmptyStringData.Values), MemberType = typeof(EmptyStringData))]
    public void Create_WithEmptyMessage_ReturnsFailure(string? message)
    {
        // Act
        var result = Notification.Create(
            new NotificationId(Guid.NewGuid()),
            TestCustomerId,
            TestCarId,
            NotificationType.Push,
            message!,
            SendTime);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(Notification.Errors.MessageEmpty);
    }

    [Fact]
    public void Create_WithTooLongMessage_ReturnsFailure()
    {
        // Arrange
        var longMsg = new string('m', Notification.MaxMessageLength + 1);

        // Act
        var result = Notification.Create(
            new NotificationId(Guid.NewGuid()),
            TestCustomerId,
            TestCarId,
            NotificationType.Email,
            longMsg,
            SendTime);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(Notification.Errors.MessageTooLong);
    }

    [Fact]
    public void ChangeStatus_ToNewStatus_ChangesStatus()
    {
        // Arrange
        var notification = Notification.Create(
            new NotificationId(Guid.NewGuid()),
            TestCustomerId,
            TestCarId,
            NotificationType.Sms,
            "Message",
            SendTime).Value;

        // Act
        var result = notification.ChangeStatus(NotificationStatus.Sent);

        // Assert
        result.IsSuccess.Should().BeTrue();
        notification.Status.Should().Be(NotificationStatus.Sent);
    }

    [Fact]
    public void ChangeStatus_ToSameStatus_ReturnsSuccess()
    {
        // Arrange
        var notification = Notification.Create(
            new NotificationId(Guid.NewGuid()),
            TestCustomerId,
            TestCarId,
            NotificationType.Sms,
            "Message",
            SendTime).Value;

        // Act
        var result = notification.ChangeStatus(NotificationStatus.Pending);

        // Assert
        result.IsSuccess.Should().BeTrue();
        notification.Status.Should().Be(NotificationStatus.Pending);
    }
}

