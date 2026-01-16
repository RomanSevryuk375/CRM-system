using CRMSystem.Business.Services;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using FluentAssertions;
using Moq;

namespace CRMSystem.Business.Tests.UnitTests;

public class TaxServiceTests
{
    private readonly Mock<ITaxRepository> _taxRepoMock;
    private readonly Mock<ITaxTypeRepository> _taxTypeRepoMock;
    private readonly Mock<ILogger<TaxService>> _loggerMock;
    private readonly TaxService _service;

    public TaxServiceTests()
    {
        _taxRepoMock = new Mock<ITaxRepository>();
        _taxTypeRepoMock = new Mock<ITaxTypeRepository>();
        _loggerMock = new Mock<ILogger<TaxService>>();

        _service = new TaxService(
            _taxRepoMock.Object,
            _taxTypeRepoMock.Object,
            _loggerMock.Object);
    }

    [Fact]
    public async Task CreateTax_ShouldThrowNotFoundException_WhenTaxTypeDoesNotExist()
    {
        var tax = ValidObjects.CreateValidTax();

        _taxTypeRepoMock.Setup(x => x.Exists(
            (int)tax.TypeId,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var act = () => _service.CreateTax(tax, CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>();

        _taxRepoMock.Verify(x => x.Create(
            It.IsAny<Tax>(),
            It.IsAny<CancellationToken>()),
            Times.Never);
    }
}
