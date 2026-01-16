using CRMSystem.Business.Services;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using FluentAssertions;
using Moq;

namespace CRMSystem.Business.Tests.UnitTests;

public class SupplierServiceTests
{
    private readonly Mock<ISupplierRepository> _supplierRepoMock;
    private readonly Mock<ILogger<SupplierService>> _loggerMock;
    private readonly SupplierService _service;

    public SupplierServiceTests()
    {
        _supplierRepoMock = new Mock<ISupplierRepository>();
        _loggerMock = new Mock<ILogger<SupplierService>>();

        _service = new SupplierService(
            _supplierRepoMock.Object,
            _loggerMock.Object);
    }

    [Fact]
    public async Task CreateSupplier_ShouldThrowConflictException_WhenExistByName()
    {
        var supplier = ValidObjects.CreateValidSupplier();

        _supplierRepoMock.Setup(x => x.ExistsByName(
            supplier.Name,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var act = () => _service.CreateSupplier(supplier, CancellationToken.None);

        await act.Should().ThrowAsync<ConflictException>();

        _supplierRepoMock.Verify(x => x.Create(
            It.IsAny<Supplier>(),
            It.IsAny<CancellationToken>()),
            Times.Never);
    }
}
