using AutoMapper;
using CRMSystem.Core.ProjectionModels.Work;
using CRMSystem.DataAccess.Entities;
using Shared.Contracts.Work;

namespace CRM_system_backend.MapProfiles;

public class WorkProfile : Profile
{
    public WorkProfile()
    {
        CreateMap<WorkItem, WorkResponse>();

        CreateMap<WorkEntity, WorkItem>();
        
        CreateMap<WorkRequest, WorkCreateModel>();
        
        CreateMap<WorkUpdateRequest, WorkUpdateModel>();
    }
}
