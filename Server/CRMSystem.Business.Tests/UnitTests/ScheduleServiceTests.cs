using CRMSystem.Business.Services;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using FluentAssertions;
using Moq;

namespace CRMSystem.Business.Tests.UnitTests;

public class ScheduleServiceTests
{
    private readonly Mock<IScheduleRepository> _shceduleRepoMock;
    private readonly Mock<IWorkerRepository> _workerRepoMock;
    private readonly Mock<IShiftRepository> _shiftRepoMock;
    private readonly Mock<ILogger<ScheduleService>> _loggerMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly ScheduleService _service;

    public ScheduleServiceTests()
    {
        _shceduleRepoMock = new Mock<IScheduleRepository>();
        _workerRepoMock = new Mock<IWorkerRepository>();
        _shiftRepoMock = new Mock<IShiftRepository>();
        _loggerMock = new Mock<ILogger<ScheduleService>>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _service = new ScheduleService(
            _shceduleRepoMock.Object,
            _workerRepoMock.Object,
            _shiftRepoMock.Object,
            _loggerMock.Object,
            _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task CreateSchedule_ShouldThrowNotFoundException_WhenWorkerDoesNotExist()
    {
        var schedule = ValidObjects.CreateValidSchedul();

        _workerRepoMock.Setup(x => x.Exists(
            schedule.WorkerId,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var act = () => _service.CreateSchedule(schedule, CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>();

        _shceduleRepoMock.Verify(x => x.Create(
            It.IsAny<Schedule>(),
            It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task CreateSchedule_ShouldThrowNotFoundException_WhenShiftDoesNotExist()
    {
        var schedule = ValidObjects.CreateValidSchedul();

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

        _shceduleRepoMock.Verify(x => x.Create(
            It.IsAny<Schedule>(),
            It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task CreateSchedule_ShouldRollback_WhenShiftDoesNotExist()
    {
        var schedule = ValidObjects.CreateValidSchedul();
        var shift = ValidObjects.CreateValidShift();

        _workerRepoMock.Setup(x => x.Exists(
            schedule.WorkerId,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        _shiftRepoMock.Setup(x => x.Create(
            shift,
            It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Some think going wrong"));

        _shiftRepoMock.Setup(x => x.Exists(
            schedule.ShiftId,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var act = () => _service.CreateWithShift(schedule, shift, CancellationToken.None);

        await act.Should().ThrowAsync<Exception>();

        _shceduleRepoMock.Verify(x => x.Create(
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
}
