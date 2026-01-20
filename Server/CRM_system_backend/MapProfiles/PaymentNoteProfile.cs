using AutoMapper;
using CRMSystem.Core.ProjectionModels.PaymentNote;
using CRMSystem.DataAccess.Entites;
using Shared.Contracts.PaymentNote;

namespace CRM_system_backend.MapProfiles;

public class PaymentNoteProfile : Profile
{
    public PaymentNoteProfile()
    {
        CreateMap<PaymentNoteItem, PaymentNoteResponse>();

        CreateMap<PaymentNoteEntity, PaymentNoteItem>();
    }
}
