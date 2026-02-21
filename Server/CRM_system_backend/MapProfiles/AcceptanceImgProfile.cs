// Ignore Spelling: Img

using AutoMapper;
using CRMSystem.Core.ProjectionModels.AccetanceImg;
using CRMSystem.DataAccess.Entites;
using Shared.Contracts.AcceptanceImg;

namespace CRM_system_backend.MapProfiles;

public class AcceptanceImgProfile : Profile
{
    public AcceptanceImgProfile()
    {
        CreateMap<AcceptanceImgItem, AcceptanceImgResponse>();

        CreateMap<AcceptanceImgEntity, AcceptanceImgItem>();
    }
}
