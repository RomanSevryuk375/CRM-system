using CRMSystem.Business.Services;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using FluentAssertions;
using Moq;

namespace CRMSystem.Business.Tests.UnitTests;

public class PartServiceTests
{
    private readonly Mock<IPartRepository> _partRepoMock;
    private readonly Mock<IPartCategoryRepository> _partCategoryRepoMock;
    private readonly Mock<ILogger<PartService>> _loggerMock;
    private readonly PartService _service;

    public PartServiceTests()
    {
        _partRepoMock = new Mock<IPartRepository>();
        _partCategoryRepoMock = new Mock<IPartCategoryRepository>();
        _loggerMock = new Mock<ILogger<PartService>>();

        _service = new PartService(
            _partRepoMock.Object,
            _partCategoryRepoMock.Object,
            _loggerMock.Object);
    }

    [Fact]
    public async Task CreatePart_ShouldThrowNotFoundException_WhenCategoryDoesNotExist()
    {
        var part = ValidObjects.CreateValidPart();

        _partCategoryRepoMock.Setup(x => x.Exists(
            part.CategoryId,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var act = () => _service.CreatePart(part, CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>();

        _partRepoMock.Verify(x => x.Create(
            It.IsAny<Part>(),
            It.IsAny<CancellationToken>()),
            Times.Never);
    }
}
