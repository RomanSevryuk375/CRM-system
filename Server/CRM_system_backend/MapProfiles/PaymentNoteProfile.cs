using AutoMapper;
using CRM_system_backend.Contracts.PaymentNote;
using CRMSystem.Core.DTOs.PaymentNote;
using CRMSystem.DataAccess.Entites;

namespace CRM_system_backend.MapProfiles;

public class PaymentNoteProfile : Profile
{
    public PaymentNoteProfile()
    {
        CreateMap<PaymentNoteItem, PaymentNoteResponse>();

        CreateMap<PaymentNoteEntity, PaymentNoteItem>();
    }
}
