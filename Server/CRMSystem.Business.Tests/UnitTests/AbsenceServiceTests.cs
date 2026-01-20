using CRMSystem.Business.Abstractions;
using CRMSystem.Business.Services;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using CRMSystem.Core.ProjectionModels.Absence;
using FluentAssertions;
using Moq;
using Shared.Enums;

namespace CRMSystem.Business.Tests.UnitTests;

public class AbsenceServiceTests
{
    private readonly Mock<IAbsenceRepository> _absenceRepoMock;
    private readonly Mock<IWorkerRepository> _workerRepoMock;
    private readonly Mock<IUserContext> _userContextMock;
    private readonly Mock<ILogger<AbsenceService>> _loggerMock;
    private readonly AbsenceService _service;

    public AbsenceServiceTests()
    {
        _absenceRepoMock = new Mock<IAbsenceRepository>();
        _workerRepoMock = new Mock<IWorkerRepository>();
        _userContextMock = new Mock<IUserContext>();
        _loggerMock = new Mock<ILogger<AbsenceService>>();

        _service = new AbsenceService(
            _absenceRepoMock.Object,
            _workerRepoMock.Object,
            _userContextMock.Object,
            _loggerMock.Object);
    }

    [Theory]

    [InlineData("2025-01-05", "2025-01-10", "2025-01-07", "2025-01-08", true)]

    [InlineData("2025-01-07", "2025-01-14", "2025-01-05", "2025-01-08", true)]

    [InlineData("2025-01-01", "2025-01-05", "2025-01-06", "2025-01-10", false)]

    [InlineData("2025-01-10", null, "2025-01-15", "2025-01-20", true)]
    public void OverlapsWith_ShouldIdentifyOverlapCorrectly(
        string existingStart, string? existingEnd,
        string newStart, string newEnd,
        bool expectedResult)
    {
        var (absence, errors) = Absence.Create(1, 123, AbsenceTypeEnum.Vacation,
            DateOnly.Parse(existingStart), existingEnd != null ? DateOnly.Parse(existingEnd) : null);

        absence.Should().NotBeNull();
        errors.Should().BeEmpty();

        var newStartDate = DateOnly.Parse(newStart);
        DateOnly? newEndDate = newEnd != null ? DateOnly.Parse(newEnd) : null;

        var result = absence.OverlapsWith(newStartDate, newEndDate);

        result.Should().Be(expectedResult);
    }

    [Fact] 
    public async Task CreateAbsence_ShouldThrowNotFoundException_WhenWorkerDoesNotExist()
    {
        var absence = ValidObjects.CreateValidAbsence(null);

        _workerRepoMock.Setup(x => x.Exists(
                            absence.WorkerId,
                            It.IsAny<CancellationToken>()))
                       .ReturnsAsync(false);

        _absenceRepoMock.Setup(x => x.GetByWorkerId(
                            absence.WorkerId,
                            It.IsAny<CancellationToken>()))
                        .ReturnsAsync([]);

        var act = () => _service.CreateAbsence(absence, CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>();

        _absenceRepoMock.Verify(x => x.Create(
                            It.IsAny<Absence>(),
                            It.IsAny<CancellationToken>()),
                            Times.Never);
    }

    [Fact]
    public async Task CreateAbsence_ShouldThrowConflictException_WhenDatesOverlap()
    {
        var absence = ValidObjects.CreateValidAbsence(null);
        var newAbsence = ValidObjects.CreateValidAbsence(new DateOnly(2025, 1, 7));

        _workerRepoMock.Setup(x => x.Exists(
                            absence.WorkerId,
                            It.IsAny<CancellationToken>()))
                        .ReturnsAsync(true);

        _absenceRepoMock.Setup(x => x.GetByWorkerId(
                            absence.WorkerId,
                            It.IsAny<CancellationToken>()))
                        .ReturnsAsync([newAbsence]);

        var act = () => _service.CreateAbsence(absence, CancellationToken.None);

        await act.Should().ThrowAsync<ConflictException>();

        _absenceRepoMock.Verify(x => x.Create(
                            It.IsAny<Absence>(),
                            It.IsAny<CancellationToken>()),
                            Times.Never);
    }

    [Fact]
    public async Task CreateAbsence_WhenWorkerExistsAndDoesNotOverlap_ShouldReturnId()
    {
        var absenceId = 0;
        var absence = ValidObjects.CreateValidAbsence(null);

        _workerRepoMock.Setup(x => x.Exists(
                            absence.WorkerId,
                            It.IsAny<CancellationToken>()))
                        .ReturnsAsync(true);

        _absenceRepoMock.Setup(x => x.GetByWorkerId(
                            absence.WorkerId,
                            It.IsAny<CancellationToken>()))
                        .ReturnsAsync([]);

        _absenceRepoMock.Setup(x => x.Create(
                            absence,
                            It.IsAny<CancellationToken>()))
                        .ReturnsAsync(absenceId);

        var result = await _service.CreateAbsence(absence, CancellationToken.None);

        result.Should().Be(absenceId);

        _absenceRepoMock.Verify(x => x.Create(
                            It.IsAny<Absence>(),
                            It.IsAny<CancellationToken>()),
                            Times.Once);
    }

    [Fact]
    public async Task UpdateAbsence_ShouldThrowConflictException_WhenDatesOverlap()
    {
        var absenceId = 10;
        var workerId = 1;

        var model = new AbsenceUpdateModel
        {
            TypeId = AbsenceTypeEnum.SickLeave,
            StartDate = new DateOnly(2025, 1, 1),
            EndDate = new DateOnly(2025, 1, 14)
        };

        var newAbsence = ValidObjects.CreateValidAbsence(new DateOnly(2025, 1, 7));

        _absenceRepoMock.Setup(x => x.GetWorkerId(
                            absenceId,
                            It.IsAny<CancellationToken>()))
                        .ReturnsAsync(workerId);

        _absenceRepoMock.Setup(x => x.GetByWorkerId(
                            workerId,
                            It.IsAny<CancellationToken>()))
                        .ReturnsAsync([newAbsence]);

        var act = () => _service.UpdateAbsence(absenceId, model, It.IsAny<CancellationToken>());

        await act.Should().ThrowAsync<ConflictException>();

        _absenceRepoMock.Verify(x => x.Update(
                            absenceId,
                            model,
                            It.IsAny<CancellationToken>()),
                            Times.Never);
    }

    [Fact]
    public async Task DeleteAbsence_ShouldReturnId_WhichIEnterInStart()
    {
        var absenceId = 10;

        _absenceRepoMock.Setup(x => x.Delete(absenceId, It.IsAny<CancellationToken>()))
                        .ReturnsAsync(absenceId);

        var result = await _service.DeleteAbsence(
                        absenceId, 
                        CancellationToken.None);

        result.Should().Be(absenceId);

        _absenceRepoMock.Verify(x => x.Delete(
                            absenceId,
                            It.IsAny<CancellationToken>()), 
                            Times.Once);
    }

    [Fact]
    public async Task GetPagedAbsence_WhenUserIsNotManager_ShouldForceFilterByOwnProfileId()
    {
        var inputFilter = new AbsenceFilter(
            [1, 2, 3], 
            null, 
            1,
            5,
            true);

        _userContextMock.Setup(x => x.ProfileId).Returns(10);
        _userContextMock.Setup(x => x.RoleId).Returns(3);


        _absenceRepoMock.Setup(x => x.GetPaged(
            It.IsAny<AbsenceFilter>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<AbsenceItem>());

        await _service.GetPagedAbsence(inputFilter, CancellationToken.None);

        _absenceRepoMock.Verify(x => x.GetPaged(
                It.Is<AbsenceFilter>(f => f.WorkerIds!.Count() == 1 && f.WorkerIds!.First() == 10),
                It.IsAny<CancellationToken>()),
                Times.Once);
    }

    [Fact]
    public async Task GetPagedAbsence_WhenUserIsManager_ShouldNotForceFilterByOwnProfileId()
    {
        var inputFilter = new AbsenceFilter(
            [1, 2, 3],
            null,
            1,
            5,
            true);

        _userContextMock.Setup(x => x.ProfileId).Returns(10);
        _userContextMock.Setup(x => x.RoleId).Returns(1);


        _absenceRepoMock.Setup(x => x.GetPaged(
            It.IsAny<AbsenceFilter>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<AbsenceItem>());

        await _service.GetPagedAbsence(inputFilter, CancellationToken.None);

        _absenceRepoMock.Verify(x => x.GetPaged(
                It.Is<AbsenceFilter>(f => f.WorkerIds!.Count() != 1 && f.WorkerIds!.First() == 1),
                It.IsAny<CancellationToken>()),
                Times.Once);
    }
}
