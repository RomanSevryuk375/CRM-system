using AutoMapper;
using CRM_system_backend.Contracts.Guarantee;
using CRMSystem.Core.ProjectionModels.Guarantee;
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
