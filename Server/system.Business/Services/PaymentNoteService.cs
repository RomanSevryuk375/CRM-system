using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.PaymentNote;
using CRMSystem.Core.Enums;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Services;

public class PaymentNoteService : IPaymentNoteService
{
    private readonly IBillRepository _billRepository;
    private readonly IPaymentNoteRepository _paymentNoteRepository;
    private readonly IPaymentMethodRepository _paymentMethodRepository;
    private readonly ILogger<PaymentNoteService> _logger;

    public PaymentNoteService(
        IBillRepository billRepository,
        IPaymentNoteRepository paymentNoteRepository,
        IPaymentMethodRepository paymentMethodRepository,
        ILogger<PaymentNoteService> logger)
    {
        _billRepository = billRepository;
        _paymentNoteRepository = paymentNoteRepository;
        _paymentMethodRepository = paymentMethodRepository;
        _logger = logger;
    }

    public async Task<List<PaymentNoteItem>> GetPagedPaymentNotes(PaymentNoteFilter filter, CancellationToken ct)
    {
        _logger.LogInformation("Getting payment note start");

        var paymentsNote = await _paymentNoteRepository.GetPaged(filter, ct);

        _logger.LogInformation("Getting payment note success");

        return paymentsNote;
    }

    public async Task<int> GetCountPaymentNotes(PaymentNoteFilter filter, CancellationToken ct)
    {
        _logger.LogInformation("Getting count payment note start");

        var count = await _paymentNoteRepository.GetCount(filter, ct);

        _logger.LogInformation("Getting count payment note success");

        return count;
    }

    public async Task<long> CreatePaymentNote(PaymentNote paymentNote, CancellationToken ct)
    {
        _logger.LogInformation("Creating payment note start");

        if (!await _billRepository.Exists(paymentNote.BillId, ct))
        {
            _logger.LogError("Bill{billId} not found", paymentNote.BillId);
            throw new NotFoundException($"Bill {paymentNote.BillId} not found");
        }

        if (!await _paymentMethodRepository.Exists((int)paymentNote.MethodId, ct))
        {
            _logger.LogError("Method {mrthodId} not found", (int)paymentNote.MethodId);
            throw new NotFoundException($"Method {(int)paymentNote.MethodId} not found");
        }

        var Id = await _paymentNoteRepository.Create(paymentNote, ct);

        _logger.LogInformation("Creating payment note success");

        _logger.LogInformation("Recalculating bill{billId} start", paymentNote.BillId);

        await _billRepository.RecalculateDebt(paymentNote.BillId, ct);

        _logger.LogInformation("Recalculating bill{billId} success", paymentNote.BillId);

        return Id;
    }

    public async Task<long> UpratePaymentNote(long id, PaymentMethodEnum? method, CancellationToken ct)
    {
        _logger.LogInformation("Updating payment note start");

        if (method.HasValue && !await _paymentMethodRepository.Exists((int)method, ct))
        {
            _logger.LogError("Method {mrthodId} not found", (int)method);
            throw new NotFoundException($"Method {(int)method} not found");
        }

        var Id = await _paymentNoteRepository.Update(id, method, ct);

        _logger.LogInformation("Updating payment note success");

        return Id;
    }

    public async Task<long> DeletePaymentNote(long id, CancellationToken ct)
    {
        _logger.LogInformation("Deleting payment note start");

        var Id = await _paymentNoteRepository.Delete(id, ct);

        _logger.LogInformation("Deleting payment note success");

        return Id;
    }
}
