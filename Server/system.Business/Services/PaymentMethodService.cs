using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Services;

public class PaymentMethodService(
    IPaymentMethodRepository paymentMethodRepository,
    ILogger<PaymentMethodService> logger) : IPaymentMethodService
{
    public async Task<List<PaymentMethodItem>> GetPaymentMethods(CancellationToken ct)
    {
        logger.LogInformation("Getting payment method start");

        var paymentMethods = await paymentMethodRepository.Get(ct);

        logger.LogInformation("Getting payment method success");

        return paymentMethods;
    }
}
