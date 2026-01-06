using CRMSystem.Core.ProjectionModels;

namespace CRMSystem.Business.Abstractions;

public interface IPaymentMethodService
{
    Task<List<PaymentMethodItem>> GetPaymentMethods();
}