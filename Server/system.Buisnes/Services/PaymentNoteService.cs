using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs.PaymentNote;
using CRMSystem.Core.Enums;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Repositories;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Buisnes.Services;

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

    public async Task<List<PaymentNoteItem>> GetPagedPaymentNotes(PaymentNoteFilter filter)
    {
        _logger.LogInformation("Getting payment note start");

        var paymentsNote = await _paymentNoteRepository.GetPaged(filter);

        _logger.LogInformation("Getting payment note success");

        return paymentsNote;
    }

    public async Task<int> GetCountPaymentNotes(PaymentNoteFilter filter)
    {
        _logger.LogInformation("Getting count payment note start");

        var count = await _paymentNoteRepository.GetCount(filter);

        _logger.LogInformation("Getting count payment note success");

        return count;
    }

    public async Task<long> CreatePaymentNote(PaymentNote paymentNote)
    {
        _logger.LogInformation("Creating payment note start");

        if (!await _billRepository.Exists(paymentNote.BillId))
        {
            _logger.LogError("Bill{billId} not found", paymentNote.BillId);
            throw new NotFoundException($"Bill {paymentNote.BillId} not found");
        }

        if (!await _paymentMethodRepository.Exists((int)paymentNote.MethodId))
        {
            _logger.LogError("Method {mrthodId} not found", (int)paymentNote.MethodId);
            throw new NotFoundException($"Method {(int)paymentNote.MethodId} not found");
        }

        var Id = await _paymentNoteRepository.Create(paymentNote);

        _logger.LogInformation("Creating payment note success");

        _logger.LogInformation("Recalculating bill{billId} start", paymentNote.BillId);

        await _billRepository.RecalculateDebt(paymentNote.BillId);

        _logger.LogInformation("Recalculating bill{billId} succes", paymentNote.BillId);

        return Id;
    }

    public async Task<long> UpratePaymentNote(long id, PaymentMethodEnum? method)
    {
        _logger.LogInformation("Updating payment note start");

        if (method.HasValue && !await _paymentMethodRepository.Exists((int)method))
        {
            _logger.LogError("Method {mrthodId} not found", (int)method);
            throw new NotFoundException($"Method {(int)method} not found");
        }

        var Id = await _paymentNoteRepository.Update(id, method);

        _logger.LogInformation("Updating payment note success");

        return Id;
    }

    public async Task<long> DeletePaymentNote(long id)
    {
        _logger.LogInformation("Deleting payment note start");

        var Id = await _paymentNoteRepository.Delete(id);

        _logger.LogInformation("Deleting payment note success");

        return Id;
    }
}
