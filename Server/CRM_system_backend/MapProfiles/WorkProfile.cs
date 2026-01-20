using AutoMapper;
using CRMSystem.Core.ProjectionModels.Work;
using CRMSystem.DataAccess.Entites;
using Shared.Contracts.Work;

namespace CRM_system_backend.MapProfiles;

public class WorkProfile : Profile
{
    public WorkProfile()
    {
        CreateMap<WorkItem, WorkResponse>();

        CreateMap<WorkEntity, WorkItem>();
    }
}
