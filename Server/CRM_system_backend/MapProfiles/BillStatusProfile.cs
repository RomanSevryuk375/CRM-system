using AutoMapper;
using CRM_system_backend.Contracts;
using CRMSystem.Core.ProjectionModels;
using CRMSystem.DataAccess.Entites;

namespace CRM_system_backend.MapProfiles;

public class BillStatusProfile : Profile
{
    public BillStatusProfile()
    {
        CreateMap<BillStatusItem, BillStatusResponse>();

        CreateMap<BillStatusEntity, BillStatusItem>();
    }
}
