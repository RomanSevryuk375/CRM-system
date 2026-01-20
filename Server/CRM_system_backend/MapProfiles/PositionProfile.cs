using AutoMapper;
using CRMSystem.Core.ProjectionModels.Position;
using CRMSystem.DataAccess.Entites;
using Shared.Contracts.Position;

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
