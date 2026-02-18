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

    // Этот тест проверяет логику домена напрямую, его менять не нужно
    [Theory]
    [InlineData("2025-01-05", "2025-01-10", "2025-01-07", "2025-01-08", true)]
    [InlineData("2025-01-07", "2025-01-14", "2025-01-05", "2025-01-08", true)]
    [InlineData("2025-01-01", "2025-01-05", "2025-01-06", "2025-01-10", false)]
    [InlineData("2025-01-10", null, "2025-01-15", "2025-01-20", true)]
    public void OverlapsWith_ShouldIdentifyOverlapCorrectly(
        string existingStart, string? existingEnd,
        string newStart, string? newEnd,
        bool expectedResult)
    {
        var (absence, errors) = Absence.Create(
            1,
            123,
            AbsenceTypeEnum.Vacation,
            DateOnly.Parse(existingStart), 
            existingEnd != null ? DateOnly.Parse(existingEnd) : null);

        absence.Should().NotBeNull();
        var newStartDate = DateOnly.Parse(newStart);
        DateOnly? newEndDate = newEnd != null ? DateOnly.Parse(newEnd) : null;

        var result = absence!.OverlapsWith(newStartDate, newEndDate);

        result.Should().Be(expectedResult);
    }

    [Fact] 
    public async Task CreateAbsence_ShouldThrowNotFoundException_WhenWorkerDoesNotExist()
    {
        var createModel = new AbsenceCreateModel(
            1,
            AbsenceTypeEnum.Vacation,
            new DateOnly(2025,1,1),
            null);

        _workerRepoMock.Setup(x => x.Exists(
                createModel.WorkerId,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        _absenceRepoMock.Setup(x => x.GetByWorkerId(
                createModel.WorkerId, 
                It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);
        
        var act = () => _service.CreateAbsence(
            createModel, 
            CancellationToken.None);
        
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task CreateAbsence_ShouldThrowConflictException_WhenDatesOverlap()
    {
        var createModel = new AbsenceCreateModel(
            1,
            AbsenceTypeEnum.Vacation,
            new DateOnly(2025, 1, 7),
            null);
        
        var existingAbsence = ValidObjects.CreateValidAbsence(
            new DateOnly(2025, 1, 10));

        _workerRepoMock.Setup(x => x.Exists(
                createModel.WorkerId,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        _absenceRepoMock.Setup(x => x.GetByWorkerId(
                createModel.WorkerId,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync([existingAbsence]);
        
        var act = () => _service.CreateAbsence(
            createModel, 
            CancellationToken.None);
        
        await act.Should().ThrowAsync<ConflictException>();
    }

    [Fact]
    public async Task CreateAbsence_ShouldThrowValidationException_WhenDomainValidationFails()
    {
        var invalidModel = new AbsenceCreateModel(
            1,
            AbsenceTypeEnum.Vacation,
            new DateOnly(2025, 1, 10),
            new DateOnly(2025, 1, 1));

        _workerRepoMock.Setup(x => x.Exists(
                invalidModel.WorkerId, 
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
        
        _absenceRepoMock.Setup(x => x.GetByWorkerId(
                invalidModel.WorkerId,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);
        
        var act = () => _service.CreateAbsence(
            invalidModel, 
            CancellationToken.None);
        
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task CreateAbsence_WhenAllValid_ShouldReturnId()
    {
        const int absenceId = 100;
        var createModel = new AbsenceCreateModel(
            1, 
            AbsenceTypeEnum.Vacation,
            new DateOnly(2025, 1, 1),
            null);

        _workerRepoMock.Setup(x => x.Exists(
                createModel.WorkerId,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        _absenceRepoMock.Setup(x => x.GetByWorkerId(
                createModel.WorkerId,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);

        _absenceRepoMock.Setup(x => x.Create(
                It.IsAny<Absence>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(absenceId);
        
        var result = await _service.CreateAbsence(
            createModel, 
            CancellationToken.None);
        
        result.Should().Be(absenceId);
        _absenceRepoMock.Verify(x => x.Create(
            It.Is<Absence>(a => a.WorkerId == createModel.WorkerId 
                                && a.StartDate == createModel.StartDate),
            It.IsAny<CancellationToken>()), 
            Times.Once);
    }

    [Fact]
    public async Task UpdateAbsence_ShouldThrowConflictException_WhenDatesOverlap()
    {
        const int absenceId = 10;
        const int workerId = 1;
        var model = new AbsenceUpdateModel
        {
            TypeId = AbsenceTypeEnum.SickLeave,
            StartDate = new DateOnly(2025, 1, 1),
            EndDate = new DateOnly(2025, 1, 14)
        };

        var overlappingAbsence = ValidObjects.CreateValidAbsence(new DateOnly(2025, 1, 7));

        _absenceRepoMock.Setup(x => x.GetWorkerId(
                absenceId,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(workerId);

        _absenceRepoMock.Setup(x => x.GetByWorkerId(
                workerId,
                It.IsAny<CancellationToken>()))
             .ReturnsAsync([overlappingAbsence]);
        
        var act = () => _service.UpdateAbsence(
            absenceId,
            model,
            It.IsAny<CancellationToken>());
        
        await act.Should().ThrowAsync<ConflictException>();
    }

    [Fact]
    public async Task DeleteAbsence_ShouldReturnId()
    {
        const int absenceId = 10;
        _absenceRepoMock.Setup(x => x.Delete(
                absenceId,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(absenceId);
        
        var result = await _service.DeleteAbsence(
            absenceId,
            CancellationToken.None);
        
        result.Should().Be(absenceId);
    }
    
}