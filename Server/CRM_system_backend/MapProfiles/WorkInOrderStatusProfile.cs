using AutoMapper;
using CRMSystem.Core.ProjectionModels;
using CRMSystem.DataAccess.Entites;
using Shared.Contracts;

namespace CRM_system_backend.MapProfiles;

public class WorkInOrderStatusProfile : Profile
{
    public WorkInOrderStatusProfile()
    {
        CreateMap<WorkInOrderStatusItem, WorkInOrderStatusResponse>();

        CreateMap<WorkInOrderStatusEntity, WorkInOrderStatusItem>();
    }
}
