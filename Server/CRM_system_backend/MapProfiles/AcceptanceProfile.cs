using AutoMapper;
using CRM_system_backend.Contracts.Acceptance;
using CRMSystem.Core.DTOs.Acceptance;
using CRMSystem.DataAccess.Entites;

namespace CRM_system_backend.MapProfiles;

public class AcceptanceProfile : Profile
{
    public AcceptanceProfile()
    {
        CreateMap<AcceptanceEntity, AcceptanceItem>()
            .ForMember(desc => desc.Worker,
                        opt => opt.MapFrom(src => $"{src.Worker!.Name} {src.Worker.Surname}"));

        CreateMap<AcceptanceItem, AcceptanceResponse>();

        CreateMap<AcceptanceUpdateRequest, AcceptanceUpdateModel>();
    }
}
