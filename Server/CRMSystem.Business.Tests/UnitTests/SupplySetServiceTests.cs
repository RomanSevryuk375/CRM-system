using CRMSystem.Business.Services;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using FluentAssertions;
using Moq;

namespace CRMSystem.Business.Tests.UnitTests;

public class SupplySetServiceTests
{
    private readonly Mock<ISupplyRepository> _supplyRepoMock;
    private readonly Mock<ISupplySetRepository> _supplySetRepoMock;
    private readonly Mock<IPositionRepository> _positionRepoMock;
    private readonly Mock<ILogger<SupplySetService>> _loggerMock;
    private readonly SupplySetService _service;

    public SupplySetServiceTests()
    {
        _supplyRepoMock = new Mock<ISupplyRepository>();
        _supplySetRepoMock = new Mock<ISupplySetRepository>();
        _positionRepoMock = new Mock<IPositionRepository>();
        _loggerMock = new Mock<ILogger<SupplySetService>>();

        _service = new SupplySetService(
            _supplySetRepoMock.Object,
            _supplyRepoMock.Object,
            _positionRepoMock.Object,
            _loggerMock.Object);
    }

    [Fact]
    public async Task CreateSupplySet_ShouldThrowNotFoundException_WhenSupplyDoesNotExist()
    {
        var supplySet = ValidObjects.CreateValidSupplySet();

        _supplyRepoMock.Setup(x => x.Exists(
            supplySet.SupplyId,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var act = () => _service.CreateSupplySet(supplySet, CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>();

        _supplySetRepoMock.Verify(x => x.Create(
            It.IsAny<SupplySet>(),
            It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task CreateSupplySet_ShouldThrowNotFoundException_WhenPositionDoesNotExist()
    {
        var supplySet = ValidObjects.CreateValidSupplySet();

        _supplyRepoMock.Setup(x => x.Exists(
            supplySet.SupplyId,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        _positionRepoMock.Setup(x => x.Exists(
            supplySet.PositionId,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var act = () => _service.CreateSupplySet(supplySet, CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>();

        _supplySetRepoMock.Verify(x => x.Create(
            It.IsAny<SupplySet>(),
            It.IsAny<CancellationToken>()),
            Times.Never);
    }
}
