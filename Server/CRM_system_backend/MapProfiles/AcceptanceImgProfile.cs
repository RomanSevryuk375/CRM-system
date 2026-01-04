using AutoMapper;
using CRMSystem.Core.DTOs.AcceptanceImg;
using CRMSystem.Core.DTOs.AccetanceImg;
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
