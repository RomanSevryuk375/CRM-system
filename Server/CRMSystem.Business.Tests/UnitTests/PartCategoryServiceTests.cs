using CRMSystem.Business.Services;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using FluentAssertions;
using Moq;

namespace CRMSystem.Business.Tests.UnitTests;

public class PartCategoryServiceTests
{
    private readonly Mock<IPartCategoryRepository> _partCategoryRepoMock;
    private readonly Mock<ILogger<PartCategoryService>> _loggerMock;
    private readonly PartCategoryService _service;

    public PartCategoryServiceTests()
    {
        _partCategoryRepoMock = new Mock<IPartCategoryRepository>();
        _loggerMock = new Mock<ILogger<PartCategoryService>>();

        _service = new PartCategoryService(
            _partCategoryRepoMock.Object,
            _loggerMock.Object);
    }

    [Fact]
    public async Task CreatePartCategory_ShouldThrowConflictException_WhenCategoryNameTaken()
    {
        var category = ValidObjects.CreateValidPartCategory();

        _partCategoryRepoMock.Setup(x => x.NameExists(
            category.Name,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var act = () => _service.CreatePartCategory(category, CancellationToken.None);

        await act.Should().ThrowAsync<ConflictException>();

        _partCategoryRepoMock.Verify(x => x.Create(
            It.IsAny<PartCategory>(),
            It.IsAny<CancellationToken>()),
            Times.Never);
    }
}
