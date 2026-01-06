using AutoMapper;
using CRM_system_backend.Contracts.Supply;
using CRMSystem.Core.ProjectionModels.Supply;
using CRMSystem.DataAccess.Entites;

namespace CRM_system_backend.MapProfiles;

public class SupplyProfile : Profile
{
    public SupplyProfile()
    {
        CreateMap<SupplyItem, SupplyResponse>();

        CreateMap<SupplyEntity, SupplyItem>()
            .ForMember(dest => dest.Supplier,
                        opt => opt.MapFrom(src => $"{src.Supplier!.Name}"));
    }
}
