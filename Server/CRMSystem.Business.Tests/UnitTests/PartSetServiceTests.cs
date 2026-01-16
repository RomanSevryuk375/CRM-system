using CRMSystem.Business.Services;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using FluentAssertions;
using Moq;

namespace CRMSystem.Business.Tests.UnitTests;

public class PartSetServiceTests
{
    private readonly Mock<IOrderRepository> _orderRepoMock;
    private readonly Mock<IPositionRepository> _positionRepoMock;
    private readonly Mock<IWorkProposalRepository> _workProposalRepoMock;
    private readonly Mock<IPartSetRepository> _partSetRepoMock;
    private readonly Mock<IBillRepository> _billRepoMock;
    private readonly Mock<ILogger<PartSetService>> _loggerMock;
    private readonly PartSetService _service;

    public PartSetServiceTests()
    {
        _orderRepoMock = new Mock<IOrderRepository>();
        _positionRepoMock = new Mock<IPositionRepository>();
        _workProposalRepoMock = new Mock<IWorkProposalRepository>();
        _partSetRepoMock = new Mock<IPartSetRepository>();
        _billRepoMock = new Mock<IBillRepository>();
        _loggerMock = new Mock<ILogger<PartSetService>>();

        _service = new PartSetService(
            _orderRepoMock.Object,
            _positionRepoMock.Object,
            _workProposalRepoMock.Object,
            _partSetRepoMock.Object,
            _billRepoMock.Object,
            _loggerMock.Object);
    }

    [Fact]
    public async Task AddToPartSet_ShouldThrowNotFoundException_WhenOrderDoesNotExist()
    {
        var partSet = ValidObjects.CreateValidPartSet(1, null);

        _orderRepoMock.Setup(x => x.Exists(
            partSet.OrderId!.Value,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var act = () => _service.AddToPartSet(partSet, CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>();

        _partSetRepoMock.Verify(x => x.Create(
            It.IsAny<PartSet>(),
            It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task AddToPartSet_ShouldThrowNotFoundException_WhenWorkProposalDoesNotExist()
    {
        var partSet = ValidObjects.CreateValidPartSet(null, 1);

        _workProposalRepoMock.Setup(x => x.Exists(
            partSet.ProposalId!.Value,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var act = () => _service.AddToPartSet(partSet, CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>();

        _partSetRepoMock.Verify(x => x.Create(
            It.IsAny<PartSet>(),
            It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task AddToPartSet_ShouldThrowNotFoundException_WhenPositionDoesNotExist()
    {
        var partSet = ValidObjects.CreateValidPartSet(1, null);

        _orderRepoMock.Setup(x => x.Exists(
            partSet.OrderId!.Value,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        _positionRepoMock.Setup(x => x.Exists(
            partSet.PositionId,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var act = () => _service.AddToPartSet(partSet, CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>();

        _partSetRepoMock.Verify(x => x.Create(
            It.IsAny<PartSet>(),
            It.IsAny<CancellationToken>()),
            Times.Never);
    }
}
