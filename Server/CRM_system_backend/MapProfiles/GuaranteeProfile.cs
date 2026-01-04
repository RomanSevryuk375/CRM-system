using AutoMapper;
using CRMSystem.Core.DTOs.Guarantee;
using CRMSystem.DataAccess.Entites;

namespace CRM_system_backend.MapProfiles;

public class GuaranteeProfile : Profile
{
    public GuaranteeProfile()
    {
        CreateMap<GuaranteeItem, GuaranteeResponse>();

        CreateMap<GuaranteeEntity, GuaranteeItem>();
    }
}
