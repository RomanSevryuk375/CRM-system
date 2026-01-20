using AutoMapper;
using CRMSystem.Core.ProjectionModels;
using CRMSystem.DataAccess.Entites;
using Shared.Contracts;

namespace CRM_system_backend.MapProfiles;

public class BillStatusProfile : Profile
{
    public BillStatusProfile()
    {
        CreateMap<BillStatusItem, BillStatusResponse>();

        CreateMap<BillStatusEntity, BillStatusItem>();
    }
}
