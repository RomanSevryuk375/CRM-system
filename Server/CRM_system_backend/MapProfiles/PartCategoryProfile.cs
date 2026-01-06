using AutoMapper;
using CRM_system_backend.Contracts.PartCategory;
using CRMSystem.Core.ProjectionModels.PartCategory;
using CRMSystem.DataAccess.Entites;

namespace CRM_system_backend.MapProfiles;

public class PartCategoryProfile : Profile
{
    public PartCategoryProfile()
    {
        CreateMap<PartCategoryItem, PartCategoryResponse>();

        CreateMap<PartCategoryEntity, PartCategoryItem>();
    }
}
