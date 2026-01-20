using AutoMapper;
using CRMSystem.Core.ProjectionModels.Supply;
using CRMSystem.DataAccess.Entites;
using Shared.Contracts.Supply;

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
