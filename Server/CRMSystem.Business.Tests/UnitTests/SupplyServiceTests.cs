using CRMSystem.Business.Services;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using FluentAssertions;
using Moq;

namespace CRMSystem.Business.Tests.UnitTests;

public class SupplyServiceTests
{
    private readonly Mock<ISupplyRepository> _supplyRepoMock;
    private readonly Mock<ISupplierRepository> _supplierRepoMock;
    private readonly Mock<ILogger<SupplyService>> _loggerMock;
    private readonly SupplyService _service;

    public SupplyServiceTests()
    {
        _supplyRepoMock = new Mock<ISupplyRepository>();
        _supplierRepoMock = new Mock<ISupplierRepository>();
        _loggerMock = new Mock<ILogger<SupplyService>>();

        _service = new SupplyService(
            _supplyRepoMock.Object,
            _supplierRepoMock.Object,
            _loggerMock.Object);
    }

    [Fact]
    public async Task CreateSupply_ShouldThrowNotFoundException_WhenSupplierDoesNotExist()
    {
        var supply = ValidObjects.CreateValidSupply();

        _supplierRepoMock.Setup(x => x.Exists(
            supply.SupplierId,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var act = () => _service.CreateSupply(supply, CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>();

        _supplyRepoMock.Verify(x => x.Create(
            It.IsAny<Supply>(),
            It.IsAny<CancellationToken>()),
            Times.Never);
    }
}
