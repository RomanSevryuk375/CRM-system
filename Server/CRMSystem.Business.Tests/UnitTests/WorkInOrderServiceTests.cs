using CRMSystem.Business.Abstractions;
using CRMSystem.Business.Services;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using CRMSystem.Core.ProjectionModels.Schedule;
using CRMSystem.Core.ProjectionModels.WorkInOrder;
using FluentAssertions;
using Moq;
using Shared.Filters;

namespace CRMSystem.Business.Tests.UnitTests;

public class WorkInOrderServiceTests
{
    private readonly Mock<IWorkInOrderRepository> _wioRepoMock;
    private readonly Mock<IWorkInOrderStatusRepository> _wioStatusRepoMock;
    private readonly Mock<IWorkerRepository> _workerRepoMock;
    private readonly Mock<IOrderRepository> _orderRepoMock;
    private readonly Mock<IWorkRepository> _workRepoMock;
    private readonly Mock<IBillRepository> _billRepoMock;
    private readonly Mock<IUserContext> _userContextMock;
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
        _userContextMock = new Mock<IUserContext>();
        _loggerMock = new Mock<ILogger<WorkInOrderService>>();

        _service = new WorkInOrderService(
            _wioRepoMock.Object,
            _wioStatusRepoMock.Object,
            _workerRepoMock.Object,
            _orderRepoMock.Object,
            _workRepoMock.Object,
            _billRepoMock.Object,
            _userContextMock.Object,
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

    [Fact]
    public async Task GetPagedWiO_WhenUserIsNotManager_ShouldForceFilterByOwnProfileId()
    {
        var inputFilter = new WorkInOrderFilter(
            [],
            [],
            [1, 2, 3],
            [],
            null,
            1,
            5,
            true);

        _userContextMock.Setup(x => x.ProfileId).Returns(10);
        _userContextMock.Setup(x => x.RoleId).Returns(3);


        _wioRepoMock.Setup(x => x.GetPaged(
            It.IsAny<WorkInOrderFilter>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<WorkInOrderItem>());

        await _service.GetPagedWiO(inputFilter, CancellationToken.None);

        _wioRepoMock.Verify(x => x.GetPaged(
                It.Is<WorkInOrderFilter>(f => f.WorkerIds!.Count() == 1 && f.WorkerIds!.First() == 10),
                It.IsAny<CancellationToken>()),
                Times.Once);
    }

    [Fact]
    public async Task GetPagedWiO_WhenUserIsManager_ShouldNotForceFilterByOwnProfileId()
    {
        var inputFilter = new WorkInOrderFilter(
            [],
            [],
            [1, 2, 3],
            [],
            null,
            1,
            5,
            true);

        _userContextMock.Setup(x => x.ProfileId).Returns(10);
        _userContextMock.Setup(x => x.RoleId).Returns(1);


        _wioRepoMock.Setup(x => x.GetPaged(
            It.IsAny<WorkInOrderFilter>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<WorkInOrderItem>());

        await _service.GetPagedWiO(inputFilter, CancellationToken.None);

        _wioRepoMock.Verify(x => x.GetPaged(
                It.Is<WorkInOrderFilter>(f => f.WorkerIds!.Count() != 1 && f.WorkerIds!.First() == 1),
                It.IsAny<CancellationToken>()),
                Times.Once);
    }
}
