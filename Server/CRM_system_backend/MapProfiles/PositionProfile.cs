using AutoMapper;
using CRM_system_backend.Contracts.Position;
using CRMSystem.Core.ProjectionModels.Position;
using CRMSystem.DataAccess.Entites;

namespace CRM_system_backend.MapProfiles;

public class PositionProfile : Profile
{
    public PositionProfile()
    {
        CreateMap<PositionItem, PositionResponse>();

        CreateMap<PositionEntity, PositionItem>()
            .ForMember(dest => dest.Part,
                        opt => opt.MapFrom(src => $"{src.Part!.Name}"));
    }
}
