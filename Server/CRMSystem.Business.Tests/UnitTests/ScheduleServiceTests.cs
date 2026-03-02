using CRMSystem.Business.Abstractions;
using CRMSystem.Business.Services;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using CRMSystem.Core.ProjectionModels.Schedule;
using FluentAssertions;
using Moq;
using Shared.Filters;

namespace CRMSystem.Business.Tests.UnitTests;

public class ScheduleServiceTests
{
    private readonly Mock<IScheduleRepository> _scheduleRepoMock;
    private readonly Mock<IWorkerRepository> _workerRepoMock;
    private readonly Mock<IShiftRepository> _shiftRepoMock;
    private readonly Mock<IUserContext> _userContextMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly ScheduleService _service;

    public ScheduleServiceTests()
    {
        _scheduleRepoMock = new Mock<IScheduleRepository>();
        _workerRepoMock = new Mock<IWorkerRepository>();
        _shiftRepoMock = new Mock<IShiftRepository>();
        var loggerMock = new Mock<ILogger<ScheduleService>>();
        _userContextMock = new Mock<IUserContext>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _service = new ScheduleService(
            _scheduleRepoMock.Object,
            _workerRepoMock.Object,
            _shiftRepoMock.Object,
            _userContextMock.Object,
            loggerMock.Object,
            _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task CreateSchedule_ShouldThrowNotFoundException_WhenWorkerDoesNotExist()
    {
        var schedule = ValidObjects.CreateValidSchedule();

        _workerRepoMock.Setup(x => x.Exists(
            schedule.WorkerId,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var act = () => _service.CreateSchedule(schedule, CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>();

        _scheduleRepoMock.Verify(x => x.Create(
            It.IsAny<Schedule>(),
            It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task CreateSchedule_ShouldThrowNotFoundException_WhenShiftDoesNotExist()
    {
        var schedule = ValidObjects.CreateValidSchedule();

        _workerRepoMock.Setup(x => x.Exists(
            schedule.WorkerId,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        _shiftRepoMock.Setup(x => x.Exists(
            schedule.ShiftId,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var act = () => _service.CreateSchedule(schedule, CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>();

        _scheduleRepoMock.Verify(x => x.Create(
            It.IsAny<Schedule>(),
            It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task CreateSchedule_ShouldRollback_WhenShiftDoesNotExist()
    {
        var schedule = ValidObjects.CreateValidSchedule();
        var shift = ValidObjects.CreateValidShift();

        _workerRepoMock.Setup(x => x.Exists(
            schedule.WorkerId,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        _shiftRepoMock.Setup(x => x.Create(
            It.IsAny<Shift>(),
            It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Some think going wrong"));

        _shiftRepoMock.Setup(x => x.Exists(
            schedule.ShiftId,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var act = () => _service.CreateWithShift(schedule, shift, CancellationToken.None);

        await act.Should().ThrowAsync<Exception>();

        _scheduleRepoMock.Verify(x => x.Create(
            It.IsAny<Schedule>(),
            It.IsAny<CancellationToken>()),
            Times.Never);

        _unitOfWorkMock.Verify(x => x.BeginTransactionAsync(
                     It.IsAny<CancellationToken>()),
                    Times.Once);

        _unitOfWorkMock.Verify(x => x.CommitTransactionAsync(
                         It.IsAny<CancellationToken>()),
                        Times.Never);

        _unitOfWorkMock.Verify(x => x.RollbackAsync(
                         It.IsAny<CancellationToken>()),
                        Times.Once);
    }

    [Fact]
    public async Task GetPagedSchedules_WhenUserIsNotManager_ShouldForceFilterByOwnProfileId()
    {
        var inputFilter = new ScheduleFilter(
            [1, 2, 3],
            [],
            null,
            1,
            5,
            true);

        _userContextMock.Setup(x => x.ProfileId).Returns(10);
        _userContextMock.Setup(x => x.RoleId).Returns(3);


        _scheduleRepoMock.Setup(x => x.GetPaged(
            It.IsAny<ScheduleFilter>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<ScheduleItem>());

        await _service.GetPagedSchedules(inputFilter, CancellationToken.None);

        _scheduleRepoMock.Verify(x => x.GetPaged(
                It.Is<ScheduleFilter>(f => f.WorkerIds!.Count() == 1 && f.WorkerIds!.First() == 10),
                It.IsAny<CancellationToken>()),
                Times.Once);
    }

    [Fact]
    public async Task GetPagedSchedules_WhenUserIsManager_ShouldNotForceFilterByOwnProfileId()
    {
        var inputFilter = new ScheduleFilter(
            [1, 2, 3],
            [],
            null,
            1,
            5,
            true);

        _userContextMock.Setup(x => x.ProfileId).Returns(10);
        _userContextMock.Setup(x => x.RoleId).Returns(1);


        _scheduleRepoMock.Setup(x => x.GetPaged(
            It.IsAny<ScheduleFilter>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<ScheduleItem>());

        await _service.GetPagedSchedules(inputFilter, CancellationToken.None);

        _scheduleRepoMock.Verify(x => x.GetPaged(
                It.Is<ScheduleFilter>(f => f.WorkerIds!.Count() != 1 && f.WorkerIds!.First() == 1),
                It.IsAny<CancellationToken>()),
                Times.Once);
    }
}
