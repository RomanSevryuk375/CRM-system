using AutoMapper;
using CRMSystem.Core.ProjectionModels.Guarantee;
using CRMSystem.DataAccess.Entites;
using Shared.Contracts.Guarantee;

namespace CRM_system_backend.MapProfiles;

public class GuaranteeProfile : Profile
{
    public GuaranteeProfile()
    {
        CreateMap<GuaranteeItem, GuaranteeResponse>();

        CreateMap<GuaranteeEntity, GuaranteeItem>();
    }
}
