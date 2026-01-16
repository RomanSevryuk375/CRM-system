using CRMSystem.Business.Services;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using FluentAssertions;
using Moq;

namespace CRMSystem.Business.Tests.UnitTests;

public class WorkInOrderServiceTests
{
    private readonly Mock<IWorkInOrderRepository> _wioRepoMock;
    private readonly Mock<IWorkInOrderStatusRepository> _wioStatusRepoMock;
    private readonly Mock<IWorkerRepository> _workerRepoMock;
    private readonly Mock<IOrderRepository> _orderRepoMock;
    private readonly Mock<IWorkRepository> _workRepoMock;
    private readonly Mock<IBillRepository> _billRepoMock;
    private readonly Mock<ILogger<WorkInOrderService>> _loggerMock;
    private readonly WorkInOrderService _service;

    public WorkInOrderServiceTests()
    {
        _wioRepoMock = new Mock<IWorkInOrderRepository>();
        _wioStatusRepoMock = new Mock<IWorkInOrderStatusRepository>();
        _workerRepoMock = new Mock<IWorkerRepository>();
        _orderRepoMock = new Mock<IOrderRepository>();
        _workRepoMock = new Mock<IWorkRepository>();
        _billRepoMock = new Mock<IBillRepository>();
        _loggerMock = new Mock<ILogger<WorkInOrderService>>();

        _service = new WorkInOrderService(
            _wioRepoMock.Object,
            _wioStatusRepoMock.Object,
            _workerRepoMock.Object,
            _orderRepoMock.Object,
            _workRepoMock.Object,
            _billRepoMock.Object,
            _loggerMock.Object);
    }

    [Fact]
    public async Task CreateWiO_ShouldThrowNotFoundException_WhenOrderDoesNotExist()
    {
        var wio = ValidObjects.CreateValidWorkInOrder();

        _orderRepoMock.Setup(x => x.Exists(
            wio.OrderId,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var act = () => _service.CreateWiO(wio, CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>();

        _wioRepoMock.Verify(x => x.Create(
            It.IsAny<WorkInOrder>(),
            It.IsAny<CancellationToken>()),
            Times.Never());
    }

    [Fact]
    public async Task CreateWiO_ShouldThrowNotFoundException_WhenWorkerDoesNotExist()
    {
        var wio = ValidObjects.CreateValidWorkInOrder();

        _orderRepoMock.Setup(x => x.Exists(
            wio.OrderId,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        _workerRepoMock.Setup(x => x.Exists(
            wio.WorkerId,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var act = () => _service.CreateWiO(wio, CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>();

        _wioRepoMock.Verify(x => x.Create(
            It.IsAny<WorkInOrder>(),
            It.IsAny<CancellationToken>()),
            Times.Never());
    }

    [Fact]
    public async Task CreateWiO_ShouldThrowNotFoundException_WhenStatusDoesNotExist()
    {
        var wio = ValidObjects.CreateValidWorkInOrder();

        _orderRepoMock.Setup(x => x.Exists(
            wio.OrderId,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        _workerRepoMock.Setup(x => x.Exists(
            wio.WorkerId,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        _wioStatusRepoMock.Setup(x => x.Exists(
            (int)wio.StatusId,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var act = () => _service.CreateWiO(wio, CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>();

        _wioRepoMock.Verify(x => x.Create(
            It.IsAny<WorkInOrder>(),
            It.IsAny<CancellationToken>()),
            Times.Never());
    }

    [Fact]
    public async Task CreateWiO_ShouldThrowNotFoundException_WhenWorkDoesNotExist()
    {
        var wio = ValidObjects.CreateValidWorkInOrder();

        _orderRepoMock.Setup(x => x.Exists(
            wio.OrderId,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        _workerRepoMock.Setup(x => x.Exists(
            wio.WorkerId,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        _wioStatusRepoMock.Setup(x => x.Exists(
            (int)wio.StatusId,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        _workRepoMock.Setup(x => x.Exists(
            wio.JobId,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var act = () => _service.CreateWiO(wio, CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>();

        _wioRepoMock.Verify(x => x.Create(
            It.IsAny<WorkInOrder>(),
            It.IsAny<CancellationToken>()),
            Times.Never());
    }
}
