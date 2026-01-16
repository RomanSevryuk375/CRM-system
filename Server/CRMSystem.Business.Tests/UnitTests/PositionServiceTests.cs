using CRMSystem.Business.Services;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using CRMSystem.Core.ProjectionModels.Position;
using FluentAssertions;
using Moq;

namespace CRMSystem.Business.Tests.UnitTests;

public class PositionServiceTests
{
    private readonly Mock<IPositionRepository> _positionRepoMock;
    private readonly Mock<IPartRepository> _partRepoMock;
    private readonly Mock<IStorageCellRepository> _storageCellRepoMock;
    private readonly Mock<IPartCategoryRepository> _partCategoryRepoMock;
    private readonly Mock<ILogger<PositionService>> _loggerMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly PositionService _service;

    public PositionServiceTests()
    {
        _positionRepoMock = new Mock<IPositionRepository>();
        _partRepoMock = new Mock<IPartRepository>();
        _storageCellRepoMock = new Mock<IStorageCellRepository>();
        _partCategoryRepoMock = new Mock<IPartCategoryRepository>();    
        _loggerMock = new Mock<ILogger<PositionService>>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _service = new PositionService(
            _positionRepoMock.Object,
            _partRepoMock.Object,
            _storageCellRepoMock.Object,
            _partCategoryRepoMock.Object,
            _loggerMock.Object,
            _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task CreatePositionWithPart_ShouldThrowNotFoundException_WhenPartCategoryDoesNotExist()
    {
        var position = ValidObjects.CreateValidPosition();
        var part = ValidObjects.CreateValidPart();

        _partCategoryRepoMock.Setup(x => x.Exists(
            part.CategoryId,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var act = () => _service.CreatePositionWithPart(position, part, CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>();

        _partRepoMock.Verify(x => x.Create(
            It.IsAny<Part>(),
            It.IsAny<CancellationToken>()),
            Times.Never);

        _positionRepoMock.Verify(x => x.Create(
            It.IsAny<Position>(),
            It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task CreatePositionWithPart_ShouldRollbackTransaction_WhenSomeThinkGoingWrong()
    {
        var positionId = 0;
        var position = ValidObjects.CreateValidPosition();
        var part = ValidObjects.CreateValidPart();

        _partCategoryRepoMock.Setup(x => x.Exists(
            part.CategoryId,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        _storageCellRepoMock.Setup(x => x.Exists(
            position.CellId,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        _partRepoMock.Setup(x => x.Create(
            part,
            It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Some think going wrong"));

        _positionRepoMock.Setup(x => x.Create(
            position,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(positionId);

        var act = () => _service.CreatePositionWithPart(position, part, CancellationToken.None);

        await act.Should().ThrowAsync<Exception>();

        _partRepoMock.Verify(x => x.Create(
            It.IsAny<Part>(),
            It.IsAny<CancellationToken>()),
            Times.Once);

        _positionRepoMock.Verify(x => x.Create(
            It.IsAny<Position>(),
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
    public async Task CreatePositionWithPart_ShouldThrowNotFoundException_WhenStorageCellDoesNotExist()
    {
        var position = ValidObjects.CreateValidPosition();
        var part = ValidObjects.CreateValidPart();

        _partCategoryRepoMock.Setup(x => x.Exists(
            part.CategoryId,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        _storageCellRepoMock.Setup(x => x.Exists(
            position.CellId,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var act = () => _service.CreatePositionWithPart(position, part, CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>();

        _partRepoMock.Verify(x => x.Create(
            It.IsAny<Part>(),
            It.IsAny<CancellationToken>()),
            Times.Once);

        _positionRepoMock.Verify(x => x.Create(
            It.IsAny<Position>(),
            It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task UpdatePosition_ShouldThrowNotFoundException_WhenStorageCellDoesNotExist()
    {
        var positionId = 123;
        var model = new PositionUpdateModel(
            123,
            null,
            null,
            null);

        _storageCellRepoMock.Setup(x => x.Exists(
            model.CellId!.Value,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var act = () => _service.UpdatePosition(positionId, model, CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>();

        _positionRepoMock.Verify(x => x.Update(
            It.IsAny<int>(),
            It.IsAny<PositionUpdateModel>(),
            It.IsAny<CancellationToken>()),
            Times.Never);
    }
}




