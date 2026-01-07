using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Services;

public class PaymentMethodService : IPaymentMethodService
{
    private readonly IPaymentMethodRepository _paymentMethodRepository;
    private readonly ILogger<PaymentMethodService> _logger;

    public PaymentMethodService(
        IPaymentMethodRepository paymentMethodRepository,
        ILogger<PaymentMethodService> logger)
    {
        _paymentMethodRepository = paymentMethodRepository;
        _logger = logger;
    }

    public async Task<List<PaymentMethodItem>> GetPaymentMethods()
    {
        _logger.LogInformation("Getting payment method start");

        var paymentMethods = await _paymentMethodRepository.Get();

        _logger.LogInformation("Getting payment method success");

        return paymentMethods;
    }
}
