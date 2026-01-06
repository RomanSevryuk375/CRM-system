using AutoMapper;
using CRM_system_backend.Contracts.Part;
using CRMSystem.Core.ProjectionModels.Part;
using CRMSystem.DataAccess.Entites;

namespace CRM_system_backend.MapProfiles;

public class PartProfile : Profile
{
    public PartProfile()
    {
        CreateMap<PartItem, PartResponse>();

        CreateMap<PartEntity, PartItem>()
            .ForMember(dest => dest.Category,
                        opt => opt.MapFrom(src => $"{src.PartCategory!.Name}"));
    }
}
