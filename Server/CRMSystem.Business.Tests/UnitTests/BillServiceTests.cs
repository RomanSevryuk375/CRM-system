using CRMSystem.Business.Services;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.Enums;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using CRMSystem.Core.ProjectionModels.Bill;
using FluentAssertions;
using Moq;

namespace CRMSystem.Business.Tests.UnitTests;

public class BillServiceTests
{
    private readonly Mock<IBillRepository> _billRepoMock;
    private readonly Mock<IOrderRepository> _orderRepoMock;
    private readonly Mock<IBillStatusRepository> _billStatusRepoMock;
    private readonly Mock<ILogger<BillService>> _loggerMock;
    private readonly BillService _service;

    public BillServiceTests()
    {
        _billRepoMock = new Mock<IBillRepository>();
        _orderRepoMock = new Mock<IOrderRepository>();
        _billStatusRepoMock = new Mock<IBillStatusRepository>();
        _loggerMock = new Mock<ILogger<BillService>>();

        _service = new BillService(
            _billRepoMock.Object,
            _orderRepoMock.Object,
            _billStatusRepoMock.Object,
            _loggerMock.Object);
    }

    [Fact]
    public async Task CreateBill_ShouldThrowNotFoundException_WhenOrderDoesNotExist()
    {
        var bill = ValidObjects.CreateValidBill();

        _orderRepoMock.Setup(x => x.Exists(
                        bill.OrderId,
                        It.IsAny<CancellationToken>()))
                       .ReturnsAsync(false);

        var act = () => _service.CreateBill(bill, CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>();

        _billRepoMock.Verify(x => x.Create(
                        It.IsAny<Bill>(), 
                        It.IsAny<CancellationToken>()), 
                       Times.Never);
    }

    [Fact]
    public async Task CreateBill_ShouldThrowConflictException_WhenOrderIsClosed()
    {
        var bill = ValidObjects.CreateValidBill();

        _orderRepoMock.Setup(x => x.Exists(
                        bill.OrderId,
                        It.IsAny<CancellationToken>()))
                       .ReturnsAsync(true);

        _orderRepoMock.Setup(x => x.GetStatus(
                        bill.OrderId,
                        It.IsAny<CancellationToken>()))
                       .ReturnsAsync((int)OrderStatusEnum.Closed);

        var act = () => _service.CreateBill(bill, CancellationToken.None);

        await act.Should().ThrowAsync<ConflictException>();

        _billRepoMock.Verify(x => x.Create(
                        It.IsAny<Bill>(),
                        It.IsAny<CancellationToken>()),
                       Times.Never);
    }

    [Fact]
    public async Task CreateBill_ShouldNotFoundException_WhenBillStatusDoesNotExist()
    {
        var bill = ValidObjects.CreateValidBill();

        _orderRepoMock.Setup(x => x.Exists(
                        bill.OrderId,
                        It.IsAny<CancellationToken>()))
                       .ReturnsAsync(true);

        _orderRepoMock.Setup(x => x.GetStatus(
                        bill.OrderId,
                        It.IsAny<CancellationToken>()))
                       .ReturnsAsync((int)OrderStatusEnum.InProgress);

        _billStatusRepoMock.Setup(x => x.Exists(
                        (int)bill.StatusId,
                        It.IsAny<CancellationToken>()))
                       .ReturnsAsync(false);

        var act = () => _service.CreateBill(bill, CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>();

        _billRepoMock.Verify(x => x.Create(
                        It.IsAny<Bill>(),
                        It.IsAny<CancellationToken>()),
                       Times.Never);
    }

    [Fact]
    public async Task CreateBill_WhenOrderExistsAndStatusNotClosedAndBillStatusExists_ShouldReturnId()
    {
        var billId = 0;
        var bill = ValidObjects.CreateValidBill();

        _orderRepoMock.Setup(x => x.Exists(
                        bill.OrderId,
                        It.IsAny<CancellationToken>()))
                       .ReturnsAsync(true);

        _orderRepoMock.Setup(x => x.GetStatus(
                        bill.OrderId,
                        It.IsAny<CancellationToken>()))
                       .ReturnsAsync((int)OrderStatusEnum.InProgress);

        _billStatusRepoMock.Setup(x => x.Exists(
                        (int)bill.StatusId,
                        It.IsAny<CancellationToken>()))
                       .ReturnsAsync(true);

        _billRepoMock.Setup(x => x.Create(
                        bill,
                        It.IsAny<CancellationToken>()))
                       .ReturnsAsync(billId);

        var result = await _service.CreateBill(bill, CancellationToken.None);

        result.Should().Be(billId);

        _billRepoMock.Verify(x => x.Create(
                        It.IsAny<Bill>(),
                        It.IsAny<CancellationToken>()),
                       Times.Once);
    }

    [Fact]
    public async Task UpdateBill_ShouldNotFoundException_WhenBillStatusDoesNotExist()
    {
        var billId = 123;
        var model = new BillUpdateModel { 
            StatusId = (BillStatusEnum)4,
            Amount = null,
            ActualBillDate = null
        };

        _billStatusRepoMock.Setup(x => x.Exists(
                        (int)model.StatusId,
                        It.IsAny<CancellationToken>()))
                       .ReturnsAsync(false);

        var act = () => _service.UpdateBill(billId, model, CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>();

        _billRepoMock.Verify(x => x.Update(
                        billId,
                        model,
                        It.IsAny<CancellationToken>()),
                       Times.Never);
    }
}
