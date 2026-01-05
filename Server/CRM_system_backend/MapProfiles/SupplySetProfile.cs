using AutoMapper;
using CRM_system_backend.Contracts.SupplySet;
using CRMSystem.Core.DTOs.SupplySet;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;

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
