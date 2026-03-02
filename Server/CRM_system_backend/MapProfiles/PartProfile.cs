using AutoMapper;
using CRMSystem.Core.ProjectionModels.Part;
using CRMSystem.DataAccess.Entities;
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

        CreateMap<PartRequest, PartCreateModel>();
        
        CreateMap<PartUpdateRequest, PartUpdateModel>();
    }
}
