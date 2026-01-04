using AutoMapper;
using CRMSystem.Core.DTOs.PartCategory;
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
