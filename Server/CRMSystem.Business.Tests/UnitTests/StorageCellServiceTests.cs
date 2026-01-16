using CRMSystem.Business.Services;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using FluentAssertions;
using Moq;

namespace CRMSystem.Business.Tests.UnitTests;

public class StorageCellServiceTests
{
    private readonly Mock<IStorageCellRepository> _storageCellRepoMock;
    private readonly Mock<ILogger<StorageCellService>> _loggerMock;
    private readonly StorageCellService _service;

    public StorageCellServiceTests()
    {
        _storageCellRepoMock = new Mock<IStorageCellRepository>();
        _loggerMock = new Mock<ILogger<StorageCellService>>();

        _service = new StorageCellService(
            _storageCellRepoMock.Object,
            _loggerMock.Object);
    }

    [Fact]
    public async Task CreateStorageCell_ShouldThrowConflictException_WhenHasOverlaps()
    {
        var cell = ValidObjects.CreateValidStorageCell();

        _storageCellRepoMock.Setup(x => x.HasOverlaps(
            cell.Rack,
            cell.Shelf,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var act = () => _service.CreateStorageCell(cell, CancellationToken.None);

        await act.Should().ThrowAsync<ConflictException>();

        _storageCellRepoMock.Verify(x => x.Create(
            It.IsAny<StorageCell>(),
            It.IsAny<CancellationToken>()),
            Times.Never);
    }
}
