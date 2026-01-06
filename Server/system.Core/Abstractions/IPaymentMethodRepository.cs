using CRMSystem.Core.ProjectionModels;

namespace CRMSystem.Core.Abstractions;

public interface IPaymentMethodRepository
{
    Task<List<PaymentMethodItem>> Get();
    Task<bool> Exists(int id);
}