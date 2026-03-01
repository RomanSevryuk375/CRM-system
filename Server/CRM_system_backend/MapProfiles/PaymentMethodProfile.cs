using AutoMapper;
using CRMSystem.Core.ProjectionModels.PaymentMethod;
using CRMSystem.DataAccess.Entities;
using Shared.Contracts;

namespace CRM_system_backend.MapProfiles;

public class PaymentMethodProfile : Profile
{
    public PaymentMethodProfile()
    {
        CreateMap<PaymentMethodItem, PaymentMethodResponse>();

        CreateMap<PaymentMethodEntity, PaymentMethodItem>();
    }
}
