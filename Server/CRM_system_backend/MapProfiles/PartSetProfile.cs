using AutoMapper;
using CRMSystem.Core.DTOs.PartSet;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;

namespace CRM_system_backend.MapProfiles;

public class PartSetProfile : Profile
{
    public PartSetProfile()
    {
        CreateMap<PartSetItem, PartSetResponse>();

        CreateMap<PartSetEntity, PartSetItem>()
            .ForMember(dest => dest.Position,
                        opt => opt.MapFrom(src => $"{src.Position!.Part!.Name}"));
    }
}
