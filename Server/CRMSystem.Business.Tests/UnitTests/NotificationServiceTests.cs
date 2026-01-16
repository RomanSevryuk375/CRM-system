using CRMSystem.Business.Services;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.Enums;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using FluentAssertions;
using Moq;

namespace CRMSystem.Business.Tests.UnitTests;

public class NotificationServiceTests
{
    private readonly Mock<INotificationRepository> _notificationRepoMock;
    private readonly Mock<IClientRepository> _clientRepoMock;
    private readonly Mock<ICarRepository> _carRepoMock;
    private readonly Mock<INotificationStatusRepository> _notificationStatusRepoMock;
    private readonly Mock<INotificationTypeRepository> _notificationTypeRepoMock;
    private readonly Mock<ILogger<NotificationService>> _loggerMock;
    private readonly NotificationService _service;

    public NotificationServiceTests()
    {
        _notificationRepoMock = new Mock<INotificationRepository>();
        _clientRepoMock = new Mock<IClientRepository>();
        _carRepoMock = new Mock<ICarRepository>();
        _notificationStatusRepoMock = new Mock<INotificationStatusRepository>();
        _notificationTypeRepoMock = new Mock<INotificationTypeRepository>();
        _loggerMock = new Mock<ILogger<NotificationService>>();

        _service = new NotificationService(
            _notificationRepoMock.Object,
            _clientRepoMock.Object,
            _carRepoMock.Object,
            _notificationStatusRepoMock.Object,
            _notificationTypeRepoMock.Object,
            _loggerMock.Object);
    }

    [Fact]
    public async Task CreateNotification_ShouldThrowNotFoundException_WhenClientDoesNotExist()
    {
        var notification = ValidObjects.CreateValidNotification();

        _clientRepoMock.Setup(x => x.Exists(
            notification.ClientId,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var act = () => _service.CreateNotification(notification, CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>();

        _notificationRepoMock.Verify(x => x.Create(
            It.IsAny<Notification>(), 
            It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task CreateNotification_ShouldThrowNotFoundException_WhenCarDoesNotExist()
    {
        var notification = ValidObjects.CreateValidNotification();

        _clientRepoMock.Setup(x => x.Exists(
            notification.ClientId,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        _carRepoMock.Setup(x => x.Exists(
            notification.CarId,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var act = () => _service.CreateNotification(notification, CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>();

        _notificationRepoMock.Verify(x => x.Create(
            It.IsAny<Notification>(),
            It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task CreateNotification_ShouldThrowNotFoundException_WhenStatusDoesNotExist()
    {
        var notification = ValidObjects.CreateValidNotification();

        _clientRepoMock.Setup(x => x.Exists(
            notification.ClientId,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        _carRepoMock.Setup(x => x.Exists(
            notification.CarId,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        _notificationStatusRepoMock.Setup(x => x.Exists(
            (int)notification.StatusId,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var act = () => _service.CreateNotification(notification, CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>();

        _notificationRepoMock.Verify(x => x.Create(
            It.IsAny<Notification>(),
            It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task CreateNotification_ShouldThrowNotFoundException_WhenTypeDoesNotExist()
    {
        var notification = ValidObjects.CreateValidNotification();

        _clientRepoMock.Setup(x => x.Exists(
            notification.ClientId,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        _carRepoMock.Setup(x => x.Exists(
            notification.CarId,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        _notificationStatusRepoMock.Setup(x => x.Exists(
            (int)notification.StatusId,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        _notificationTypeRepoMock.Setup(x => x.Exists(
            (int)notification.TypeId,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var act = () => _service.CreateNotification(notification, CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>();

        _notificationRepoMock.Verify(x => x.Create(
            It.IsAny<Notification>(),
            It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task CreateNotification_ShouldReturnId_WhenClientExistsAndCarExistsAndStatusExistsAndTypeExists()
    {
        var notification = ValidObjects.CreateValidNotification();
        var notificationId = 0L;

        _clientRepoMock.Setup(x => x.Exists(
            notification.ClientId,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        _carRepoMock.Setup(x => x.Exists(
            notification.CarId,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        _notificationStatusRepoMock.Setup(x => x.Exists(
            (int)notification.StatusId,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        _notificationTypeRepoMock.Setup(x => x.Exists(
            (int)notification.TypeId,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        _notificationRepoMock.Setup(x => x.Create(
            notification,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(notificationId);

        var result  = await _service.CreateNotification(notification, CancellationToken.None);

        result.Should().Be(notificationId);

        _notificationRepoMock.Verify(x => x.Create(
            It.IsAny<Notification>(),
            It.IsAny<CancellationToken>()),
            Times.Once);
    }
}
