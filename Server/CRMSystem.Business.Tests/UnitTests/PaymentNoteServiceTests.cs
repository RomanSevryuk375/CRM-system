using CRMSystem.Business.Services;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using FluentAssertions;
using Moq;
using Shared.Enums;

namespace CRMSystem.Business.Tests.UnitTests;

public class PaymentNoteServiceTests
{
    private readonly Mock<IBillRepository> _billRepoMock;
    private readonly Mock<IPaymentNoteRepository> _paymentNoteRepoMock;
    private readonly Mock<IPaymentMethodRepository> _paymentMethodRepoMock;
    private readonly Mock<ILogger<PaymentNoteService>> _loggerMock;
    private readonly PaymentNoteService _service;

    public PaymentNoteServiceTests()
    {
        _billRepoMock = new Mock<IBillRepository>();
        _paymentNoteRepoMock = new Mock<IPaymentNoteRepository>();
        _paymentMethodRepoMock = new Mock<IPaymentMethodRepository>();
        _loggerMock = new Mock<ILogger<PaymentNoteService>>();

        _service = new PaymentNoteService(
            _billRepoMock.Object,
            _paymentNoteRepoMock.Object,
            _paymentMethodRepoMock.Object,
            _loggerMock.Object);
    }

    [Fact]
    public async Task CreatePaymentNote_ShouldThrowNotFoundException_WhenBillDoesNotExist()
    {
        var paymnetNote = ValidObjects.CreateValidPaymentNote();

        _billRepoMock.Setup(x => x.Exists(
            paymnetNote.BillId,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var act = () => _service.CreatePaymentNote(paymnetNote, CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>();

        _paymentNoteRepoMock.Verify(x => x.Create(
            It.IsAny<PaymentNote>(),
            It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task CreatePaymentNote_ShouldThrowNotFoundException_WhenMethodDoesNotExist()
    {
        var paymnetNote = ValidObjects.CreateValidPaymentNote();

        _billRepoMock.Setup(x => x.Exists(
            paymnetNote.BillId,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        _paymentMethodRepoMock.Setup(x => x.Exists(
            (int)paymnetNote.MethodId,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var act = () => _service.CreatePaymentNote(paymnetNote, CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>();

        _paymentNoteRepoMock.Verify(x => x.Create(
            It.IsAny<PaymentNote>(),
            It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task UpratePaymentNote_ShouldThrowNotFoundException_WhenMethodDoesNotExist()
    {
        var paymnetNoteId = 123;
        var paymentMethod = PaymentMethodEnum.Card;

        _paymentMethodRepoMock.Setup(x => x.Exists(
            (int)paymentMethod,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var act = () => _service.UpratePaymentNote(paymnetNoteId, paymentMethod, CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>();

        _paymentNoteRepoMock.Verify(x => x.Update(
            It.IsAny<long>(),
            It.IsAny<PaymentMethodEnum>(),
            It.IsAny<CancellationToken>()),
            Times.Never);
    }
}
