using CRMSystem.Core.ProjectionModels;

namespace CRMSystem.Core.Abstractions;

public interface IPaymentMethodRepository
{
    Task<List<PaymentMethodItem>> Get(CancellationToken ct);
    Task<bool> Exists(int id, CancellationToken ct);
}