using CRMSystem.Business.Services;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using FluentAssertions;
using Moq;

namespace CRMSystem.Business.Tests.UnitTests;

public class ShiftServiceTests
{
    private readonly Mock<IShiftRepository> _shiftRepoMock;
    private readonly Mock<ILogger<ShiftService>> _loggerMock;
    private readonly ShiftService _service;

    public ShiftServiceTests()
    {
        _shiftRepoMock = new Mock<IShiftRepository>();
        _loggerMock = new Mock<ILogger<ShiftService>>();
        _service = new ShiftService(
            _shiftRepoMock.Object, 
            _loggerMock.Object);
    }

    [Fact]
    public async Task CreateShift_ShouldThrowConflictException_WhenTimesOverlap()
    {
        var shift = ValidObjects.CreateValidShift();

        _shiftRepoMock.Setup(x => x.HasOverLap(
            shift.StartAt,
            shift.EndAt,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var act = () => _service.CreateShift(shift, CancellationToken.None);

        await act.Should().ThrowAsync<ConflictException>();

        _shiftRepoMock.Verify(x => x.Create(
            It.IsAny<Shift>(), 
            It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task CreateShift_ShouldReturnId_WhenNoOverlap()
    {
        var shift = ValidObjects.CreateValidShift();
        var expectedId = 1;

        _shiftRepoMock.Setup(x => x.HasOverLap(
            shift.StartAt, 
            shift.EndAt, 
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        _shiftRepoMock.Setup(x => x.Create(
            shift, 
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedId);

        var result = await _service.CreateShift(shift, CancellationToken.None);

        result.Should().Be(expectedId);
        _shiftRepoMock.Verify(x => x.Create(
            It.IsAny<Shift>(),
            It.IsAny<CancellationToken>()),
            Times.Once);
    }
}