using AutoMapper;
using CRMSystem.Core.ProjectionModels.Acceptance;
using CRMSystem.DataAccess.Entities;
using Shared.Contracts.Acceptance;

namespace CRM_system_backend.MapProfiles;

public class AcceptanceProfile : Profile
{
    public AcceptanceProfile()
    {
        CreateMap<AcceptanceEntity, AcceptanceItem>()
            .ForMember(desc => desc.Worker,
                opt => opt.MapFrom(src => src.Worker != null
                    ? $"{src.Worker.Name} {src.Worker.Surname}"
                    : string.Empty));

        CreateMap<AcceptanceItem, AcceptanceResponse>();

        CreateMap<AcceptanceUpdateRequest, AcceptanceUpdateModel>();

        CreateMap<AcceptanceRequest, AcceptanceCreateModel>();
    }
}