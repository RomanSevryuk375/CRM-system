using AutoMapper;
using CRMSystem.Core.ProjectionModels.Worker;
using CRMSystem.DataAccess.Entities;
using Shared.Contracts.Worker;

namespace CRM_system_backend.MapProfiles;

public class WorkerProfile : Profile
{
    public WorkerProfile()
    {
        CreateMap<WorkerItem, WorkerResponse>();

        CreateMap<WorkerEntity, WorkerItem>();
        
        CreateMap<WorkerRequest, WorkerCreateModel>();
        
        CreateMap<WorkerUpdateRequest, WorkerUpdateModel>();
    }
}
