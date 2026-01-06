// Ignore Spelling: Img

using AutoMapper;
using CRM_system_backend.Contracts.AcceptanceImg;
using CRMSystem.Core.ProjectionModels.AccetanceImg;
using CRMSystem.DataAccess.Entites;

namespace CRM_system_backend.MapProfiles;

public class AcceptanceImgProfile : Profile
{
    public AcceptanceImgProfile()
    {
        CreateMap<AcceptanceImgItem, AcceptanceImgResponse>();

        CreateMap<AcceptanceImgEntity, AcceptanceImgItem>();
    }
}
