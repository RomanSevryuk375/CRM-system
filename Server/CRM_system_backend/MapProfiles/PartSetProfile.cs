using AutoMapper;
using CRMSystem.Core.ProjectionModels.PartSet;
using CRMSystem.DataAccess.Entites;
using Shared.Contracts.PartSet;

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
