using CRMSystem.Core.ProjectionModels.PaymentMethod;

namespace CRMSystem.Business.Abstractions;

public interface IPaymentMethodService
{
    Task<List<PaymentMethodItem>> GetPaymentMethods(CancellationToken ct);
    Task<PaymentMethodItem> GetPaymentMethodById(int id, CancellationToken ct);
}