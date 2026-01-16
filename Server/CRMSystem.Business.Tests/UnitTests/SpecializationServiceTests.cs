using CRMSystem.Business.Services;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using FluentAssertions;
using Moq;

namespace CRMSystem.Business.Tests.UnitTests;

public class SpecializationServiceTests
{
    private readonly Mock<ISpecializationRepository> _specRepoMock;
    private readonly Mock<ILogger<SpecializationService>> _loggerMock;
    private SpecializationService _service;

    public SpecializationServiceTests()
    {
        _specRepoMock = new Mock<ISpecializationRepository>();
        _loggerMock = new Mock<ILogger<SpecializationService>>();

        _service = new SpecializationService(
            _specRepoMock.Object,
            _loggerMock.Object);
    }

    [Fact]
    public async Task CreateSpecialization_ShouldThrowNotFoundException_WhenNameExists()
    {
        var spec = ValidObjects.CreateValidSpecialization();

        _specRepoMock.Setup(x => x.ExistsByName(
            spec.Name,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var act = () => _service.CreateSpecialization(spec, CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>();

        _specRepoMock.Verify(x => x.Create(
            It.IsAny<Specialization>(),
            It.IsAny<CancellationToken>()),
            Times.Never);
    }
}
