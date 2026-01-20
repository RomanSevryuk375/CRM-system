using AutoMapper;
using CRMSystem.Core.ProjectionModels.PartCategory;
using CRMSystem.DataAccess.Entites;
using Shared.Contracts.PartCategory;

namespace CRM_system_backend.MapProfiles;

public class PartCategoryProfile : Profile
{
    public PartCategoryProfile()
    {
        CreateMap<PartCategoryItem, PartCategoryResponse>();

        CreateMap<PartCategoryEntity, PartCategoryItem>();
    }
}
