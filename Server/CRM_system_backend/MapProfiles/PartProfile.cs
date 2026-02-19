using AutoMapper;
using CRMSystem.Core.ProjectionModels.Part;
using CRMSystem.Core.ProjectionModels.PartCategory;
using CRMSystem.DataAccess.Entites;
using Shared.Contracts.Part;
using Shared.Contracts.PartCategory;

namespace CRM_system_backend.MapProfiles;

public class PartProfile : Profile
{
    public PartProfile()
    {
        CreateMap<PartItem, PartResponse>();

        CreateMap<PartEntity, PartItem>()
            .ForMember(dest => dest.Category,
                        opt => opt.MapFrom(src => $"{src.PartCategory!.Name}"));

        CreateMap<PartCategoryRequest, PartCategoryCreateModel>();
    }
}
