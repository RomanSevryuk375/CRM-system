using CRMSystem.Core.DTOs;

namespace CRMSystem.Buisnes.Abstractions;

public interface IPaymentMethodService
{
    Task<List<PaymentMethodItem>> GetPaymentMethods();
}