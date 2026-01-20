using AutoMapper;
using CRMSystem.Core.ProjectionModels.Part;
using CRMSystem.DataAccess.Entites;
using Shared.Contracts.Part;

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
