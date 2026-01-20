using AutoMapper;
using CRMSystem.Core.ProjectionModels.SupplySet;
using CRMSystem.DataAccess.Entites;
using Shared.Contracts.SupplySet;

namespace CRM_system_backend.MapProfiles;

public class SupplySetProfile : Profile
{
    public SupplySetProfile()
    {
        CreateMap<SupplySetItem, SupplySetResponse>();

        CreateMap<SupplySetEntity, SupplySetItem>()
            .ForMember(dest => dest.Position,
                        opt => opt.MapFrom(src => $"{src.Position!.Part!.Name}"));
    }
}
