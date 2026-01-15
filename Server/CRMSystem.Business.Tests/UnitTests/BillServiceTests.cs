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
        var (bill, errors) = Bill.Create(
            123,
            123,
            BillStatusEnum.Unpaid,
            new DateTime(2025, 1, 1),
            0,
            null);

        bill.Should().NotBeNull();
        errors.Should().BeEmpty();

        _orderRepoMock.Setup(x => x.Exists(
                        bill.OrderId,
                        It.IsAny<CancellationToken>()))
                       .ReturnsAsync(false);

        await Assert.ThrowsAsync<NotFoundException>(() =>
            _service.CreateBill(bill, CancellationToken.None));

        _billRepoMock.Verify(x => x.Create(
                        bill, 
                        It.IsAny<CancellationToken>()), 
                       Times.Never);
    }

    [Fact]
    public async Task CreateBill_ShouldThrowConflictException_WhenOrderIsClosed()
    {
        var (bill, errors) = Bill.Create(
            123,
            123,
            BillStatusEnum.Unpaid,
            new DateTime(2025, 1, 1),
            0,
            null);

        bill.Should().NotBeNull();
        errors.Should().BeEmpty();

        _orderRepoMock.Setup(x => x.Exists(
                        bill.OrderId,
                        It.IsAny<CancellationToken>()))
                       .ReturnsAsync(true);

        _orderRepoMock.Setup(x => x.GetStatus(
                        bill.OrderId,
                        It.IsAny<CancellationToken>()))
                       .ReturnsAsync((int)OrderStatusEnum.Closed);

        await Assert.ThrowsAsync<ConflictException>(() =>
            _service.CreateBill(bill, CancellationToken.None));

        _billRepoMock.Verify(x => x.Create(
                        bill,
                        It.IsAny<CancellationToken>()),
                       Times.Never);
    }

    [Fact]
    public async Task CreateBill_ShouldNotFoundException_WhenBillStatusDoesNotExist()
    {
        var (bill, errors) = Bill.Create(
            123,
            123,
            BillStatusEnum.Unpaid,
            new DateTime(2025, 1, 1),
            0,
            null);

        bill.Should().NotBeNull();
        errors.Should().BeEmpty();

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

        await Assert.ThrowsAsync<NotFoundException>(() =>
            _service.CreateBill(bill, CancellationToken.None));

        _billRepoMock.Verify(x => x.Create(
                        bill,
                        It.IsAny<CancellationToken>()),
                       Times.Never);
    }

    [Fact]
    public async Task CreateBill_WhenOrderExistsAndStatusNotClosedAndBillStatusExists_ShouldReturnId()
    {
        var billId = 123;
        var (bill, errors) = Bill.Create(
            billId,
            123,
            BillStatusEnum.Unpaid,
            new DateTime(2025, 1, 1),
            0,
            null);

        bill.Should().NotBeNull();
        errors.Should().BeEmpty();

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

        Assert.Equal(result, billId);

        _billRepoMock.Verify(x => x.Create(
                        bill,
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

        await Assert.ThrowsAsync<NotFoundException>(() =>
            _service.UpdateBill(billId ,model, CancellationToken.None));

        _billRepoMock.Verify(x => x.Update(
                        billId,
                        model,
                        It.IsAny<CancellationToken>()),
                       Times.Never);
    }
}
