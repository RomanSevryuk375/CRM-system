using CRMSystem.Core.DTOs;

namespace CRMSystem.DataAccess.Repositories
{
    public interface IPaymentMethodRepository
    {
        Task<List<PaymentMethodItem>> Get();
    }
}