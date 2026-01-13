using CRMSystem.Business.Services;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.Enums;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using CRMSystem.Core.ProjectionModels.Absence;
using Microsoft.Extensions.Logging;
using Moq;

namespace CRMSystem.Business.Tests;

public class AbsenceServiceTests
{
    private readonly Mock<IAbsenceRepository> _absenceRepoMock;
    private readonly Mock<IWorkerRepository> _workerRepoMock;
    private readonly Mock<ILogger<AbsenceService>> _loggerMock;
    private readonly AbsenceService _service;

    public AbsenceServiceTests()
    {
        _absenceRepoMock = new Mock<IAbsenceRepository>();
        _workerRepoMock = new Mock<IWorkerRepository>();
        _loggerMock = new Mock<ILogger<AbsenceService>>();

        _service = new AbsenceService(
            _absenceRepoMock.Object,
            _workerRepoMock.Object,
            _loggerMock.Object);
    }

    [Fact] 
    public async Task CreateAbsence_ShouldThrowNotFoundException_WhenWorkerDoesNotExist()
    {
        var absence = new Absence(0, 99, AbsenceTypeEnum.SickLeave, new DateOnly(2025, 1, 1), null);

        _workerRepoMock.Setup(x => x.Exists(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                       .ReturnsAsync(false);

        await Assert.ThrowsAsync<NotFoundException>(() =>
            _service.CreateAbsence(absence, CancellationToken.None));
    }

    [Fact]
    public async Task CreateAbsence_ShouldThrowConflictException_WhenDatesOverlap()
    {
        var absence = new Absence(0, 1, AbsenceTypeEnum.Vacation, new DateOnly(2025, 1, 1), null);

        _workerRepoMock.Setup(x => x.Exists(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);

        _absenceRepoMock.Setup(x => x.HasOverLap(It.IsAny<int>(), It.IsAny<DateOnly>(), It.IsAny<DateOnly?>(), null, It.IsAny<CancellationToken>()))
                        .ReturnsAsync(true);

        await Assert.ThrowsAsync<ConflictException>(() =>
            _service.CreateAbsence(absence, CancellationToken.None));
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

        _absenceRepoMock.Setup(x => x.GetWorkerId(absenceId, It.IsAny<CancellationToken>()))
                        .ReturnsAsync(workerId);

        _absenceRepoMock.Setup(x => x.HasOverLap(
                            workerId,
                            It.IsAny<DateOnly>(),
                            It.IsAny<DateOnly?>(),
                            It.IsAny<int?>(), 
                            It.IsAny<CancellationToken>()))
                        .ReturnsAsync(true);

        await Assert.ThrowsAsync<ConflictException>(() =>
            _service.UpdateAbsence(absenceId, model, default));

        _absenceRepoMock.Verify(x => x.Update(
                            It.IsAny<int>(),
                            It.IsAny<AbsenceUpdateModel>(),
                            It.IsAny<CancellationToken>()),
                            Times.Never);
    }

    [Fact]
    public async Task DeleteAbsence_ShouldReturnId_WhichIEnterInStart()
    {
        var absenceId = 10;

        _absenceRepoMock.Setup(x => x.Delete(absenceId, It.IsAny<CancellationToken>()))
                        .ReturnsAsync(absenceId);

        var result = await _service.DeleteAbsence(absenceId, It.IsAny<CancellationToken>());

        Assert.Equal(absenceId, result);

        _absenceRepoMock.Verify(x => x.Delete(
                            It.IsAny<int>(),
                            It.IsAny<CancellationToken>()), 
                            Times.Once);
    }
}
