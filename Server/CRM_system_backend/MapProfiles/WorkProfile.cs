using AutoMapper;
using CRM_system_backend.Contracts.Work;
using CRMSystem.Core.ProjectionModels.Work;
using CRMSystem.DataAccess.Entites;

namespace CRM_system_backend.MapProfiles;

public class WorkProfile : Profile
{
    public WorkProfile()
    {
        CreateMap<WorkItem, WorkResponse>();

        CreateMap<WorkEntity, WorkItem>();
    }
}
