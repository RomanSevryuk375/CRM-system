using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.PaymentNote;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using Microsoft.Extensions.Logging;
using Shared.Enums;
using Shared.Filters;

namespace CRMSystem.Business.Services;

public class PaymentNoteService(
    IBillRepository billRepository,
    IPaymentNoteRepository paymentNoteRepository,
    IPaymentMethodRepository paymentMethodRepository,
    ILogger<PaymentNoteService> logger) : IPaymentNoteService
{
    public async Task<List<PaymentNoteItem>> GetPagedPaymentNotes(PaymentNoteFilter filter, CancellationToken ct)
    {
        logger.LogInformation("Getting payment note start");

        var paymentsNote = await paymentNoteRepository.GetPaged(filter, ct);

        logger.LogInformation("Getting payment note success");

        return paymentsNote;
    }

    public async Task<int> GetCountPaymentNotes(PaymentNoteFilter filter, CancellationToken ct)
    {
        logger.LogInformation("Getting count payment note start");

        var count = await paymentNoteRepository.GetCount(filter, ct);

        logger.LogInformation("Getting count payment note success");

        return count;
    }

    public async Task<long> CreatePaymentNote(PaymentNote paymentNote, CancellationToken ct)
    {
        logger.LogInformation("Creating payment note start");

        if (!await billRepository.Exists(paymentNote.BillId, ct))
        {
            logger.LogError("Bill{billId} not found", paymentNote.BillId);
            throw new NotFoundException($"Bill {paymentNote.BillId} not found");
        }

        if (!await paymentMethodRepository.Exists((int)paymentNote.MethodId, ct))
        {
            logger.LogError("Method {methodId} not found", (int)paymentNote.MethodId);
            throw new NotFoundException($"Method {(int)paymentNote.MethodId} not found");
        }

        var Id = await paymentNoteRepository.Create(paymentNote, ct);

        logger.LogInformation("Creating payment note success");

        logger.LogInformation("Recalculating bill{billId} start", paymentNote.BillId);

        await billRepository.RecalculateDebt(paymentNote.BillId, ct);

        logger.LogInformation("Recalculating bill{billId} success", paymentNote.BillId);

        return Id;
    }

    public async Task<long> UpratePaymentNote(long id, PaymentMethodEnum? method, CancellationToken ct)
    {
        logger.LogInformation("Updating payment note start");

        if (method.HasValue && !await paymentMethodRepository.Exists((int)method, ct))
        {
            logger.LogError("Method {methodId} not found", (int)method);
            throw new NotFoundException($"Method {(int)method} not found");
        }

        var Id = await paymentNoteRepository.Update(id, method, ct);

        logger.LogInformation("Updating payment note success");

        return Id;
    }

    public async Task<long> DeletePaymentNote(long id, CancellationToken ct)
    {
        logger.LogInformation("Deleting payment note start");

        var Id = await paymentNoteRepository.Delete(id, ct);

        logger.LogInformation("Deleting payment note success");

        return Id;
    }
}
